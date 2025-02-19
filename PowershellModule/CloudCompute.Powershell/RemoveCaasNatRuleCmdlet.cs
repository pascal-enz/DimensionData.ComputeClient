﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RemoveCaasNatRuleCmdlet.cs" company="">
//   
// </copyright>
// <summary>
//   The Remove NAT Rule cmdlet.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Management.Automation;
using DD.CBU.Compute.Api.Client;
using DD.CBU.Compute.Api.Client.Network;
using DD.CBU.Compute.Api.Contracts.General;
using DD.CBU.Compute.Api.Contracts.Network;

namespace DD.CBU.Compute.Powershell
{
	/// <summary>
	/// The Remove NAT Rule cmdlet.
	/// </summary>
	[Cmdlet(VerbsCommon.Remove, "CaasNatRule")]
	public class RemoveCaasNatRuleCmdlet : PsCmdletCaasBase
	{
		/// <summary>
		/// Gets or sets the network.
		/// </summary>
		[Parameter(Mandatory = true, HelpMessage = "The network that the ACL Rule exists", 
			ValueFromPipelineByPropertyName = true)]
		public NetworkWithLocationsNetwork Network { get; set; }

		/// <summary>
		/// Gets or sets the nat rule.
		/// </summary>
		[Parameter(Mandatory = true, HelpMessage = "The ACL rule to delete", ValueFromPipeline = true)]
		public NatRuleType NatRule { get; set; }

		/// <summary>
		/// The process record method.
		/// </summary>
		protected override void ProcessRecord()
		{
			base.ProcessRecord();

			try
			{
				if (!ShouldProcess(NatRule.name)) return;
				DeleteNatRule();
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
		}

		/// <summary>
		/// Removes a NAT Rule
		/// </summary>
		private void DeleteNatRule()
		{
			Status status = Connection.ApiClient.DeleteNatRule(Network.id, NatRule.id).Result;
			if (status != null)
			{
				WriteDebug(
					string.Format(
						"{0} resulted in {1} ({2}): {3}", 
						status.operation, 
						status.result, 
						status.resultCode, 
						status.resultDetail));
			}
		}
	}
}