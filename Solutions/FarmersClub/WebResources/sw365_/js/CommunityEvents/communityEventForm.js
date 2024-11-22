var SW365;
(function (SW365) {
    var OSIRIS;
    (function (OSIRIS) {
        var Common;
        (function (Common) {
            class CommunityEventsForm {
                onFormLoad(executionContext) {
                    this.executionContext = executionContext;
                    this.formContext = this.executionContext.getFormContext();
                }
                eventStartDateOnChange() {
                    const message = 'Event End Date should be greater than Event Start Date';
                    this.validateFormDates(CommunityEventsForm.Fields.startDate, CommunityEventsForm.Fields.endDate, message);
                }
                eventEndDateOnChange() {
                    const message = 'Event Start Date should be less than Event End Date';
                    this.validateFormDates(CommunityEventsForm.Fields.startDate, CommunityEventsForm.Fields.endDate, message);
                }
                registrationStartDateOnChange() {
                    this.eventRegistrationDateValidation();
                }
                registrationEndDateOnChange() {
                    this.eventRegistrationDateValidation();
                }
                eventRegistrationDateValidation() {
                    const eventStartDate = this.formContext.getAttribute(CommunityEventsForm.Fields.startDate).getValue();
                    const eventEndDate = this.formContext.getAttribute(CommunityEventsForm.Fields.endDate).getValue();
                    const registrationStartDate = this.formContext.getAttribute(CommunityEventsForm.Fields.registrationSartDate).getValue();
                    const registrationEndDate = this.formContext.getAttribute(CommunityEventsForm.Fields.registrationEndDate).getValue();
                    if (registrationEndDate && eventEndDate && registrationEndDate > eventEndDate) {
                        this.showAlertDialog('Registration End Date should be less than Event End Date', CommunityEventsForm.Fields.registrationEndDate);
                        return;
                    }
                    else if (registrationStartDate && eventEndDate && registrationStartDate > eventEndDate) {
                        this.showAlertDialog('Registration Start Date should be less than Event End Date', CommunityEventsForm.Fields.registrationSartDate);
                        return;
                    }
                    this.validateFormDates(CommunityEventsForm.Fields.registrationSartDate, CommunityEventsForm.Fields.registrationEndDate, 'Registration Start Date should be less than Registration End Date');
                }
                validateFormDates(startDateFieldName, endDateFieldName, message) {
                    const startDate = this.formContext.getAttribute(startDateFieldName).getValue();
                    const endDate = this.formContext.getAttribute(endDateFieldName).getValue();
                    if (endDate && startDate > endDate) {
                        this.showAlertDialog(message, endDateFieldName);
                    }
                }
                showAlertDialog(message, field) {
                    const alertString = { confirmButtonLable: 'OK', text: message };
                    const alertOptions = { height: 120, width: 260 };
                    Xrm.Navigation.openAlertDialog(alertString, alertOptions).then((_) => {
                        this.formContext.getAttribute(field).setValue(null);
                    });
                }
            }
            CommunityEventsForm.Fields = {
                startDate: 'sw365_eventstartdate',
                endDate: 'sw365_eventenddate',
                registrationSartDate: 'sw365_registrationstartdate',
                registrationEndDate: 'sw365_registrationenddate'
            };
            Common.CommunityEventsForm = CommunityEventsForm;
            Common.CommunityEvent = new CommunityEventsForm();
        })(Common = OSIRIS.Common || (OSIRIS.Common = {}));
    })(OSIRIS = SW365.OSIRIS || (SW365.OSIRIS = {}));
})(SW365 || (SW365 = {}));
