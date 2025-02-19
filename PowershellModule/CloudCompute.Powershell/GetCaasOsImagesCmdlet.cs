﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetCaasOsImagesCmdlet.cs" company="">
//   
// </copyright>
// <summary>
//   The get CaaS OS Images cmdlet.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using DD.CBU.Compute.Api.Client;
using DD.CBU.Compute.Api.Contracts.Image;
using DD.CBU.Compute.Api.Contracts.Network;

namespace DD.CBU.Compute.Powershell
{
	/// <summary>
	/// The get CaaS OS Images cmdlet.
	/// </summary>
	[Cmdlet(VerbsCommon.Get, "CaasOsImages")]
	[OutputType(typeof (ImagesWithDiskSpeedImage[]))]
	public class GetCaasOsImagesCmdlet : PsCmdletCaasBase
	{
		/// <summary>
		/// The network to show the images from
		/// </summary>
		[Parameter(Mandatory = false, HelpMessage = "The network to show the images from")]
		public NetworkWithLocationsNetwork Network { get; set; }

		/// <summary>
		/// Get a image OS by name
		/// </summary>
		[Parameter(Mandatory = false, Position = 0, HelpMessage = "OS name to filter")]
		public string Name { get; set; }


		/// <summary>
		/// Get a customer image by location
		/// </summary>
		[Parameter(Mandatory = false, HelpMessage = "Location to filter")]
		public string Location { get; set; }

		/// <summary>
		/// Get a customer image by imageId
		/// </summary>
		[Parameter(Mandatory = false, HelpMessage = "ImageId to filter")]
		public string ImageId { get; set; }

		/// <summary>
		/// Get a customer image by OS Id
		/// </summary>
		[Parameter(Mandatory = false, HelpMessage = "Operating System Id to filter")]
		public string OperatingSystemId { get; set; }

		/// <summary>
		/// Get a customer image by OS family
		/// </summary>
		[Parameter(Mandatory = false, HelpMessage = "Operating System family to filter")]
		public string OperatingSystemFamily { get; set; }

		/// <summary>
		/// The process record method.
		/// </summary>
		protected override void ProcessRecord()
		{
			base.ProcessRecord();

			try
			{
				if (Network != null && string.IsNullOrEmpty(Location))
				{
					Location = Network.location;
				}

				IEnumerable<ImagesWithDiskSpeedImage> resultlist = GetOsImagesTask();
				if (resultlist != null && resultlist.Any())
				{
					switch (resultlist.Count())
					{
						case 0:
							WriteError(
								new ErrorRecord(
									new ItemNotFoundException(
										"This command cannot find a matching object with the given parameters."
										), "ItemNotFoundException", ErrorCategory.ObjectNotFound, resultlist));
							break;
						case 1:
							WriteObject(resultlist.First());
							break;
						default:
							WriteObject(resultlist, true);
							break;
					}
				}
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
		/// Gets the network servers from the CaaS
		/// </summary>
		/// <returns>
		/// The images
		/// </returns>
		private IEnumerable<ImagesWithDiskSpeedImage> GetOsImagesTask()
		{
			return Connection.ApiClient.GetImages(ImageId, Name, Location, OperatingSystemId, OperatingSystemFamily).Result;
		}
	}
}