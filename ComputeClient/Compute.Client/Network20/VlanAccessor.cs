﻿namespace DD.CBU.Compute.Api.Client.Network20
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using DD.CBU.Compute.Api.Client.Interfaces;
	using DD.CBU.Compute.Api.Client.Interfaces.Network20;
	using DD.CBU.Compute.Api.Contracts.Network20;
	using DD.CBU.Compute.Api.Contracts.Requests;
	using DD.CBU.Compute.Api.Contracts.Requests.Network;

	/// <summary>	Access methods for VLAN Operations </summary>
	/// <seealso cref="T:DD.CBU.Compute.Api.Client.Interfaces.IVlan"/>
	public class VlanAccessor : IVlanAccessor
	{
		/// <summary>
		/// The Api.
		/// </summary>
		private readonly IWebApi _api;

		/// <summary>
		/// Initialises a new instance of the <see cref="VlanAccessor"/> class.
		/// </summary>
		/// <param name="api">
		/// The api.
		/// </param>
		public VlanAccessor(IWebApi api)
		{
			_api = api;
		}

		/// <summary>
		/// 	Retrieves the list of ACL rules associated with a network. This API requires your
		/// 	organization ID and the ID of the target network.
		/// </summary>		
		/// <param name="options">
		/// 			Options for controlling the operation. 
		/// </param>
		/// <param name="pagingOptions">
		/// 	Options for controlling the paging. 
		/// </param>
		/// <returns>
		/// 	The VLAN collection. 
		/// </returns>
		public async Task<IEnumerable<VlanType>> GetVlans(VlanListOptions options = null, PageableRequest pagingOptions = null)
		{
			var vlans =
				await
				_api.GetAsync<vlans>(
					ApiUris.GetVlanByOrgId(_api.OrganizationId), pagingOptions);

			return vlans.vlan;
		}

		/// <summary>
		/// 	The get VLAN list. 
		/// </summary>		
		/// <param name="id">
		/// 			  	The id. 
		/// </param>
		/// <param name="vlanName">
		/// 		  	The VLAN name. 
		/// </param>
		/// <param name="networkDomainId">
		/// 	The network domain id. 
		/// </param>
		/// <param name="pagingOptions">
		/// The paging Options.
		/// </param>
		/// <returns>
		/// 	The <see cref="Task"/>. 
		/// </returns>
		public async Task<IEnumerable<VlanType>> GetVlans(Guid id, string vlanName, Guid networkDomainId, PageableRequest pagingOptions = null)
		{
			var vlans =
				await
				_api.GetAsync<vlans>(
					ApiUris.GetVlan(_api.OrganizationId, id, vlanName, networkDomainId), pagingOptions);

			return vlans.vlan;
		}

		/// <summary>
		/// 	An IComputeApiClient extension method that gets a VLAN. 
		/// </summary>
		/// <param name="vlanId">
		/// 	The id. 
		/// </param>
		/// <returns>
		/// 	The vlan. 
		/// </returns>
		public async Task<VlanType> GetVlan(Guid vlanId)
		{
			return await _api.GetAsync<VlanType>(
				ApiUris.GetVlan(_api.OrganizationId, vlanId));
		}

		/// <summary>
		/// Deploys Virtual LAN on a network domain
		/// </summary>
		/// <param name="vlan">
		/// Virtual LAN
		/// </param>
		/// <returns>
		/// Operation status
		/// </returns>
		public async Task<ResponseType> DeployVlan(DeployVlanType vlan)
		{
			return
				await
				_api.PostAsync<DeployVlanType, ResponseType>(
					ApiUris.DeployVlan(_api.OrganizationId),
					vlan);
		}

		/// <summary>
		/// 	An IComputeApiClient extension method that deletes the vlan. 
		/// </summary>
		/// <param name="id">
		/// 	 	The id of the VLAN. 
		/// </param>
		/// <returns>
		/// 	The job from the API; 
		/// </returns>
		public async Task<ResponseType> DeleteVlan(string id)
		{
			return 
				await
					_api.PostAsync<DeleteVlanType, ResponseType>(
						ApiUris.DeleteVlan(_api.OrganizationId),
						new DeleteVlanType { id = id });
		}
	}
}
