//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SW365.OSIRIS.TableModel
{
	
	[System.Runtime.Serialization.DataContractAttribute()]
	public enum SystemUser_OutgoingEmailDeliveryMethod
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Microsoft Dynamics 365 for Outlook", 1)]
		MicrosoftDynamics365forOutlook = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("None", 0)]
		None = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Server-Side Synchronization or Email Router", 2)]
		ServerSideSynchronizationorEmailRouter = 2,
	}
}