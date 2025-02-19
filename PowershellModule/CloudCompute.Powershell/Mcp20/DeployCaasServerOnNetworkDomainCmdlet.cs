﻿using System;
using System.Management.Automation;
using DD.CBU.Compute.Api.Client;
using DD.CBU.Compute.Api.Client.Network20;
using DD.CBU.Compute.Api.Contracts.Image;
using DD.CBU.Compute.Api.Contracts.Network20;

namespace DD.CBU.Compute.Powershell.Mcp20
{
	/// <summary>
	/// The new CaaS Virtual Machine cmdlet.
	/// </summary>
	[Cmdlet(VerbsCommon.New, "CaasServerOnNetworkDomain")]
	[OutputType(typeof (ResponseType))]
	public class DeployCaasServerOnNetworkDomainCmdlet : PsCmdletCaasBase
	{
		/// <summary>
		/// The Network Domain deploy the VM
		/// </summary>
		[Parameter(Mandatory = true, ValueFromPipeline = true, 
			HelpMessage = "The network domain in which server will be deployed")]
		public NetworkDomainType NetworkDomain { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "The server name")]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		[Parameter(Mandatory = false, ValueFromPipeline = true, HelpMessage = "The server description")]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the server image.
		/// </summary>
		[Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "The server OS Image")]
		public ImagesWithDiskSpeedImage ServerImage { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether is started.
		/// </summary>
		[Parameter(Mandatory = true, ValueFromPipeline = true, HelpMessage = "The server start flag")]
		public bool IsStarted { get; set; }

		/// <summary>
		/// The administrator password of the machine
		/// </summary>
		[Parameter(Mandatory = true, HelpMessage = "The server VM administrator password")]
		public string AdminPassword { get; set; }

		/// <summary>
		/// Gets or sets the primary network.
		/// </summary>
		[Parameter(Mandatory = true, ParameterSetName = "VlanId", HelpMessage = "The server's primary network")]
		public VlanType PrimaryNetwork { get; set; }

		/// <summary>
		/// Gets or sets the primary private IP.
		/// </summary>
		[Parameter(Mandatory = true, ParameterSetName = "PrivateIp", 
			HelpMessage = "The private network private IP address that will be assigned to the machine.")]
		public string PrimaryPrivateIp { get; set; }

		/// <summary>
		/// The process record method.
		/// </summary>
		protected override void ProcessRecord()
		{
			ResponseType response = null;
			base.ProcessRecord();
			try
			{
				VlanIdOrPrivateIpType primaryNic = new VlanIdOrPrivateIpType
				{
					vlanId = PrimaryNetwork.id,
					privateIpv4 = PrimaryPrivateIp
				};


				var server = new DeployServerType
				{
					name = Name, 
					description = Description, 
					imageId = ServerImage.id, 
					start = IsStarted, 
					administratorPassword = AdminPassword, 
					networkInfo = 
						new DeployServerTypeNetworkInfo
						{
							networkDomainId =
								NetworkDomain.id, 
							primaryNic = primaryNic
						}
				};

				response = Connection.ApiClient.DeployServerOnNetworkDomain(server).Result;
			}
			catch (AggregateException ae)
			{
				ae.Handle(
					e =>
					{
						if (e is ComputeApiException)
						{
							WriteError(new ErrorRecord(e, "-2", ErrorCategory.InvalidOperation, Connection));
						}
						else
						{
// if (e is HttpRequestException)
							ThrowTerminatingError(new ErrorRecord(e, "-1", ErrorCategory.ConnectionError, Connection));
						}

						return true;
					});
			}

			WriteObject(response);
		}
	}
}