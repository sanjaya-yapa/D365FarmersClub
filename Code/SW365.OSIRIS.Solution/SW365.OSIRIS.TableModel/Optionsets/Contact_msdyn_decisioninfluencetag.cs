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
	public enum Contact_msdyn_decisioninfluencetag
	{
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Blocker", 2, "#FF0000")]
		Blocker = 2,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Decision maker", 0, "#32C100")]
		Decisionmaker = 0,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Influencer", 1, "#FFD74B")]
		Influencer = 1,
		
		[System.Runtime.Serialization.EnumMemberAttribute()]
		[OptionSetMetadataAttribute("Unknown", 3, "#E1DFDD")]
		Unknown = 3,
	}
}