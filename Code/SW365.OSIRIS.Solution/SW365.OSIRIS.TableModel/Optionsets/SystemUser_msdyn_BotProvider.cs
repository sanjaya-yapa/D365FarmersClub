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
	public enum SystemUser_msdyn_BotProvider
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("None", 2, "#0000ff", "Indicates that the user is not a bot")]
		None = 192350002,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Other", 1, "#0000ff", "Other type of bot")]
		Other = 192350001,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Virtual Agent", 0, "#0000ff", "CCI first party Bot")]
		VirtualAgent = 192350000,
	}
}