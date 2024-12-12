namespace SW365.OSIRIS.Common {
	export interface ICommunityEventFields {
		startDate: string;
		endDate: string;
		registrationSartDate: string;
		registrationEndDate: string;
		venue: string;
	}
	export class CommunityEventsForm {
		private executionContext: Xrm.Events.EventContext;
		private commonUtil: CommonUtil;
		private formContext: Xrm.FormContext;
		static Fields: ICommunityEventFields = {
			startDate: 'sw365_eventstartdate',
			endDate: 'sw365_eventenddate',
			registrationSartDate: 'sw365_registrationstartdate',
			registrationEndDate: 'sw365_registrationenddate',
			venue: 'sw365_eventvenue'
		}

		public onFormLoad(executionContext: Xrm.Events.EventContext): void {
			this.commonUtil = new CommonUtil();
			this.executionContext = executionContext;
			this.formContext = this.executionContext.getFormContext();
		}

		public onFormSaved(): void {

		}

		public eventStartDateOnChange(): void {
			const message = 'Event End Date should be greater than Event Start Date';
			this.validateFormDates(CommunityEventsForm.Fields.startDate, CommunityEventsForm.Fields.endDate, message);
		}

		public eventEndDateOnChange(): void {
			const message = 'Event Start Date should be less than Event End Date'
			this.validateFormDates(CommunityEventsForm.Fields.startDate, CommunityEventsForm.Fields.endDate, message);
		}

		public registrationStartDateOnChange(): void {
			this.eventRegistrationDateValidation();
		}	

		public registrationEndDateOnChange(): void {
			this.eventRegistrationDateValidation();
		}

		public eventRegistrationDateValidation(): void {
			const eventStartDate = this.formContext.getAttribute(CommunityEventsForm.Fields.startDate).getValue();
			const eventEndDate = this.formContext.getAttribute(CommunityEventsForm.Fields.endDate).getValue();
			const registrationStartDate = this.formContext.getAttribute(CommunityEventsForm.Fields.registrationSartDate).getValue();
			const registrationEndDate = this.formContext.getAttribute(CommunityEventsForm.Fields.registrationEndDate).getValue();

			// Event registraion end date should be less than event end date
			if (registrationEndDate && eventEndDate && registrationEndDate > eventEndDate) {
				this.showAlertDialog('Registration End Date should be less than Event End Date', CommunityEventsForm.Fields.registrationEndDate);
				return;
			} else if (registrationStartDate && eventEndDate && registrationStartDate > eventEndDate) {
				this.showAlertDialog('Registration Start Date should be less than Event End Date', CommunityEventsForm.Fields.registrationSartDate);
				return;
			}
			//registration start date should be less than registration end date
			this.validateFormDates(CommunityEventsForm.Fields.registrationSartDate, CommunityEventsForm.Fields.registrationEndDate, 'Registration Start Date should be less than Registration End Date');
		}

		private validateFormDates(startDateFieldName: string, endDateFieldName: string, message: string): void {
			const startDate = this.formContext.getAttribute(startDateFieldName).getValue();
			const endDate = this.formContext.getAttribute(endDateFieldName).getValue();
			if (endDate && startDate > endDate) {
				this.showAlertDialog(message, endDateFieldName);
			}
		}

		private showAlertDialog(message: string, field: string): void {
			const alertString = { confirmButtonLable: 'OK', text: message };
			const alertOptions = { height: 120, width: 260 }
			Xrm.Navigation.openAlertDialog(alertString, alertOptions).then(
				(_) => {
					this.formContext.getAttribute(field).setValue(null);
				}
			);
		}
	}
	export const CommunityEvent = new CommunityEventsForm();
}