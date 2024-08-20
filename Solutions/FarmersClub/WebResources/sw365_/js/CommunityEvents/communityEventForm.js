var SW365;
(function (SW365) {
    var OSIRIS;
    (function (OSIRIS) {
        var Common;
        (function (Common) {
            class CommunityEventsForm extends SW365.OSIRIS.Common.CommonUtil {
                validateFormDates() {
                    const eventStartDate = this.attribute.value(CommunityEventsForm.Fields.startDate);
                    const eventEndDate = this.attribute.value(CommunityEventsForm.Fields.endDate);
                    if (eventStartDate > eventEndDate) {
                        const alertString = { confirmButtonLabel: 'OK', text: 'Start date cannot be greater than end date.' };
                        const alertOptions = { height: 120, width: 260 };
                        Xrm.Navigation.openAlertDialog(alertString, alertOptions).then((_) => {
                            this.attribute.clearValue(CommunityEventsForm.Fields.startDate);
                        });
                    }
                }
            }
            CommunityEventsForm.Fields = {
                startDate: 'sw365_eventstartdate',
                endDate: 'sw365_eventenddate',
                registrationSartDate: 'sw365_registrationstartdate',
                registrationEndDate: 'sw365_registrationenddate'
            };
            Common.CommunityEventsForm = CommunityEventsForm;
        })(Common = OSIRIS.Common || (OSIRIS.Common = {}));
    })(OSIRIS = SW365.OSIRIS || (SW365.OSIRIS = {}));
})(SW365 || (SW365 = {}));
