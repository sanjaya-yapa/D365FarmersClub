namespace SW365.OSIRIS.Common {
	export interface ICommunityEventFields {
		startDate: string;
		endDate: string;
		registrationSartDate: string;
		registrationEndDate: string;
	}
	export class CommunityEventsForm extends SW365.OSIRIS.Common.CommonUtil {
		static Fields: ICommunityEventFields = {
			startDate: 'sw365_eventstartdate',
			endDate: 'sw365_eventenddate',
			registrationSartDate: 'sw365_registrationstartdate',
			registrationEndDate: 'sw365_registrationenddate'
		}
		constructor(formContext: Xrm.FormContext) {
			super(formContext);
		}
		public validateFormDates(): void {
			const eventStartDate = this.attribute.value(CommunityEventsForm.Fields.startDate);
			const eventEndDate = this.attribute.value(CommunityEventsForm.Fields.endDate);
			if (eventStartDate > eventEndDate) {
				const alertString = { confirmButtonLabel: 'OK', text: 'Start date cannot be greater than end date.' };
				const alertOptions = { height: 120, width: 260 }
				Xrm.Navigation.openAlertDialog(alertString, alertOptions).then(
					(_) => {
						this.attribute.clearValue(CommunityEventsForm.Fields.startDate);
					}
				);
			}
		}
	}
	export const CommunityEvent = (formContext: Xrm.FormContext): CommunityEventsForm => {
		return new CommunityEventsForm(formContext);
	}
}