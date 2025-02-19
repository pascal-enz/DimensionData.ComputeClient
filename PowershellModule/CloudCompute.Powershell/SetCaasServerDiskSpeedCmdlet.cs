﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetCaasServerDiskSpeedCmdlet.cs" company="">
//   
// </copyright>
// <summary>
//   The set server state cmdlet.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using DD.CBU.Compute.Api.Client;
using DD.CBU.Compute.Api.Contracts.General;
using DD.CBU.Compute.Api.Contracts.Server;

namespace DD.CBU.Compute.Powershell
{
	/// <summary>
	/// The set server state cmdlet.
	/// </summary>
	[Cmdlet(VerbsCommon.Set, "CaasServerDiskSpeed")]
	public class SetCaasServerDiskSpeedCmdlet : PsCmdletCaasServerBase
	{
		/// <summary>
		/// Gets or sets the scsi id.
		/// </summary>
		[Parameter(Mandatory = true, HelpMessage = "SCSI Id of the disk to be resized")]
		public int ScsiId { get; set; }

		/// <summary>
		/// Gets or sets the speed id.
		/// </summary>
		[Parameter(Mandatory = false, ParameterSetName = "SpeedId", 
			HelpMessage =
				"The speedId of the new disk. The available speed Id can be retrieved using (Get-CaasDataCentre).hypervisor.diskSpeed"
			)]
		public string SpeedId { get; set; }


		/// <summary>
		/// Gets or sets the speed.
		/// </summary>
		[Parameter(Mandatory = false, ParameterSetName = "DiskSpeedType", HelpMessage = "The disk speed")]
		public DiskSpeedType Speed { get; set; }


		/// <summary>
		/// The process record method.
		/// </summary>
		protected override void ProcessRecord()
		{
			SetServerTask();
			base.ProcessRecord();
		}

		/// <summary>
		/// Edit the server disk details
		/// </summary>
		private void SetServerTask()
		{
			try
			{
				if (ParameterSetName.Equals("DiskSpeedType"))
					SpeedId = Speed.ToString();

				Status status = null;

				IEnumerable<DiskWithSpeedType> disk = Server.disk.Where(d => d.scsiId == ScsiId);
				if (disk.Any())
				{
					status = Connection.ApiClient.ChangeServerDiskSpeed(Server.id, disk.ElementAt(0).id, SpeedId).Result;
				}
				else
					WriteError(new ErrorRecord(new PSArgumentException("The scsi id does not exits"), "-1", 
						ErrorCategory.InvalidArgument, null));

				if (status != null)
					WriteDebug(
						string.Format(
							"{0} resulted in {1} ({2}): {3}", 
							status.operation, 
							status.result, 
							status.resultCode, 
							status.resultDetail));
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
	}
}