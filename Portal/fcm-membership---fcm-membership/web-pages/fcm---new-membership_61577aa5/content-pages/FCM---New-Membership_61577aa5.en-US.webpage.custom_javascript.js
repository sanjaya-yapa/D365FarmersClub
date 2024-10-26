$(document).ready(function(){
    
    setPrimaryContact();
});

function setPrimaryContact(){
    let primaryContactId = $("#primarycontactid").val();
    let primaryContatName = $("#primarycontactidname").val();
    $("#primarycontactid").val(primaryContactId);
    $("#primarycontactid_name").val(primaryContatName);
    $("#primarycontactid_entityname").val("contact");
}