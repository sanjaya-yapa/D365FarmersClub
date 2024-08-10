namespace SW365.OSIRIS.Common {
	export class EventTicketPriceCommands {
		public addNewEventTicketPrice(primaryControl: any, entityLogicalName: string): void {
			const primaryEntity = primaryControl.data.entity.getEntityName();
			const primaryEntityTitle = primaryControl.data.entity.getPrimaryAttributeValue();
			const primaryEntityId = primaryControl.data.entity.getId().replace('{', '').replace('}', '');

			var pageInput: Xrm.Navigation.PageInputEntityRecord = {
				pageType: 'entityrecord',
				entityName: entityLogicalName,
				data: {
					sw365_communityevent: primaryEntityId,
					sw365_communityeventname: primaryEntityTitle,
					sw365_communityeventtype: primaryEntity
				}
			};

			const navigationOptions: Xrm.Navigation.NavigationOptions = {
				target: 2,
				width: 800,
				height: 700,
				position: 1
			}

			Xrm.Navigation.navigateTo(pageInput, navigationOptions).then((success) => {
				console.info('INFO:::Event Ticket Price dialog opened');
			}, (error) => {
				console.error('ERROR:::Cannot open dialog');
			});
		}
	}
	export const EventTicketCommand = new EventTicketPriceCommands();
}