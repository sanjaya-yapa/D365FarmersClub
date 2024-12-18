﻿namespace SW365.OSIRIS.Common {
    interface IExtendedAttribute extends Xrm.Attributes.Attribute<any> {
        getValue(): any;
        setValue(value: any): void;
        clearValue(): void;
        setRequiredLevel(requiredLevel: string): void;
        addOnChange(handler: Xrm.Events.ContextSensitiveHandler): void;
        removeOnChange(handler: Xrm.Events.ContextSensitiveHandler): void;
        getLookupValue(): { id: string, name: string, entityType: string } | null;
        // Add more attribute methods and properties as needed
    }

    interface IControl {
        setVisible(visible: boolean): void;
        getAttribute(): IExtendedAttribute;
        // Add more control methods and properties as needed
    }

    interface ITab {
        setVisible(visible: boolean): void;
        // Add more tab methods and properties as needed
    }

    interface ISection {
        setVisible(visible: boolean): void;
        // Add  more section methods and properties as needed
    }

    interface IFormContext {
        getAttribute(attributeName: string): IExtendedAttribute | undefined;
        getControl(controlName: string): IControl | undefined;
        ui: {
            setFormNotification(message: string, level: "ERROR" | "INFO" | "WARNING", uniqueId: string): boolean;
            clearFormNotification(uniqueId: string): boolean;
            tabs: {
                get(tabName: string): ITab | undefined;
                // Add more tab collection methods as needed
            };
            // Add more UI methods and properties as needed
        };
        data: {
            entity: {
                getId(): string;
                getEntityName(): string;
                // Add more entity methods as needed
            };
            save(options?: Xrm.SaveOptions): Promise<void>;
            // Add more data methods as needed
        };
        // Add more form context  methods and properties as needed
    }

    export class CommonUtil {
       
        async callCustomApiPOST(apiName: string, data: any): Promise<any> {
            const requestUrl = `${Xrm.Utility.getGlobalContext().getClientUrl()}/api/data/v9.1/${apiName}`;
            try {
                const response = await fetch(requestUrl, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json',
                        'OData-MaxVersion': '4.0',
                        'OData-Version': '4.0'
                    },
                    body: JSON.stringify(data)
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.statusText}`);
                }
                const responseData: any = await response.json();
                console.log('Custom API Response: ', responseData);
                return responseData;
            }
            catch (error) {
                console.log('Error Calling the custom Api: ', error);
                throw error;
            }
        }

        async callCustomApiGET(apiName: string, queryParams: string): Promise<any> {
            const queryString = new URLSearchParams(queryParams).toString();
            const requestUrl = `${Xrm.Utility.getGlobalContext().getClientUrl()}/api/data/v9.1/${apiName}?${queryString}`;
            try {
                const response = await fetch(requestUrl, {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json',
                        'ODATA-MaxVersion': '4.0',
                        'ODATA-Version': '4.0'
                    }
                });
                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.statusText}`);
                }
                const responseData: any = await response.json();
                console.log('Custom API Response: ', responseData);
                return responseData;
            } catch (error) {
                console.error('Error Calling the custom Api: ', error);
                throw error;
            }
        }
    }
}