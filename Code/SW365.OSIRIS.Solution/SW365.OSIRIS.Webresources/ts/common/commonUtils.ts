namespace SW365.OSIRIS.Common {
    interface IAttribute {
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
        getAttribute(): IAttribute;
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
        getAttribute(attributeName: string): IAttribute | undefined;
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

    export class AttributeWrapper implements IAttribute {
        private attribute: Xrm.Attributes.Attribute<any>;

        constructor(_attribute: Xrm.Attributes.Attribute<any>) {
            this.attribute = _attribute;
        }

        getValue(): any { return this.attribute.getValue(); }

        setValue(value: any): any { return this.attribute.setValue(value); }

        clearValue(): void { this.attribute.setValue(null); }

        setRequiredLevel(requiredLevel: string): void {
            this.attribute.setRequiredLevel(requiredLevel as Xrm.Attributes.RequirementLevel);
        }

        addOnChange(handler: Xrm.Events.ContextSensitiveHandler): void {
            this.attribute.addOnChange(handler);
        }

        removeOnChange(handler: Xrm.Events.ContextSensitiveHandler): void {
            this.attribute.removeOnChange(handler);
        }

        getLookupValue(): { id: string, name: string, entityType: string } | null {
            const lookupValue = (this.attribute as Xrm.Attributes.LookupAttribute).getValue();
            if (lookupValue && lookupValue.length > 0) {
                const { id, name, entityType } = lookupValue[0];
                return { id, name, entityType };  
            }
            return null;
        }
    }

    export class FormContext implements IFormContext {
        private formContext: Xrm.FormContext;

        constructor(_formContext: Xrm.FormContext) {
            this.formContext = _formContext;
        }

        public getAttribute(attributeName: string): IAttribute {
            return this.formContext.getAttribute(attributeName) as IAttribute;
        }
        getControl(controlName: string): IControl | undefined {
            throw new Error("Method not implemented.");
        }
        ui: {
            setFormNotification(message: string, level: "ERROR" | "INFO" | "WARNING", uniqueId: string): boolean; clearFormNotification(uniqueId: string): boolean; tabs: {
                get(tabName: string): ITab | undefined;
            };
        };
        data: {
            entity: {
                getId(): string;
                getEntityName(): string;
            }; save(options?: Xrm.SaveOptions): Promise<void>;
        };

    }
    export class CommonUtil {
        protected formContext: IFormContext;
        constructor(_formContext: IFormContext) {
            this.formContext = _formContext;
        }

        public attribute = {
            value: (fieldName: string, value?: any): any => {
                const attribute = this.formContext.getAttribute(fieldName) as IAttribute;

                if (!attribute) {
                    console.error(`Attribute ${fieldName} not found.`);
                    return null;
                }

                if (typeof value !== 'undefined') {
                    attribute.setValue(value);
                    return;
                }

                return attribute.getValue();
            },

            clearValue: (fieldName: string): void => {
                const attribute = this.formContext.getAttribute(fieldName) as IAttribute;

                if (!attribute) {
                    console.error(`Attribute ${fieldName} not found.`);
                    return;
                }

                attribute.clearValue();
            },

            setRequiredLelvel: (fieldName: string, requiredLevel: 'none' | 'required' | 'recommended'): void => {
                const attribute = this.formContext.getAttribute(fieldName) as IAttribute;

                if (!attribute) {
                    console.error(`Attribute ${fieldName} not found.`);
                    return;
                }

                attribute.setRequiredLevel(requiredLevel);
            },

            getLookupValue: (fieldName: string): { id: string, name: string, entityType: string } | null => {
                const attribute = this.formContext.getAttribute(fieldName) as IAttribute;
                if (!attribute) {
                    console.error(`Attribute ${fieldName} not found.`);
                    return;
                }

                if (attribute.getValue() && attribute.getValue().length > 0) {
                    const lookupValue = attribute.getValue()[0];
                    return {
                        id: lookupValue.id,
                        name: lookupValue.name,
                        entityType: lookupValue.entityType
                    };
                }
                return null;
            }
        };

        async callCustomApi(apiName: string, data: any): Promise<any> {
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
    }
}