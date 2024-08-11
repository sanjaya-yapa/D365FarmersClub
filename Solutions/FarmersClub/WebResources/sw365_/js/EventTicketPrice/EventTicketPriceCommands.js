var SW365;
(function (SW365) {
    var OSIRIS;
    (function (OSIRIS) {
        var Common;
        (function (Common) {
            class EventTicketPriceCommands {
                addNewEventTicketPrice(primaryControl,entityLogicalName) {
                    const primaryEntity = primaryControl.data.entity.getEntityName();
                    const primaryEntityTitle = primaryControl.data.entity.getPrimaryAttributeValue();
                    const primaryEntityId = primaryControl.data.entity.getId().replace('{', '').replace('}', '');
                    var pageInput = {
                        pageType: 'entityrecord',
                        entityName: entityLogicalName,
                            data: {
                                sw365_communityevent: primaryEntityId,
                                sw365_communityeventname: primaryEntityTitle,
                                sw365_communityeventtype: primaryEntity
                            }
                        };
                    const navigationOptions = {
                        target: 2,
                        width: 800,
                        height: 700,
                        position: 1
                    };
                    Xrm.Navigation.navigateTo(pageInput, navigationOptions).then((success) => {
                        console.info('INFO:::Event Ticket Price dialog opened');
                    }, (error) => {
                        console.error('ERROR:::Cannot open dialog');
                    });
                }
            }
            Common.EventTicketPriceCommands = EventTicketPriceCommands;
            Common.EventTicketCommand = new EventTicketPriceCommands();
        })(Common = OSIRIS.Common || (OSIRIS.Common = {}));
    })(OSIRIS = SW365.OSIRIS || (SW365.OSIRIS = {}));
})(SW365 || (SW365 = {}));
