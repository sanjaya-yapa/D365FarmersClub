var SW365;
(function (SW365) {
    var OSIRIS;
    (function (OSIRIS) {
        var Common;
        (function (Common) {
            class CommonUtil {
                constructor(_formContext) {
                    this.attribute = {
                        value: (fieldName, value) => {
                            const attribute = this.formContext.getAttribute(fieldName);
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
                        clearValue: (fieldName) => {
                            const attribute = this.formContext.getAttribute(fieldName);
                            if (!attribute) {
                                console.error(`Attribute ${fieldName} not found.`);
                                return;
                            }
                            attribute.clearValue();
                        },
                        setRequiredLelvel: (fieldName, requiredLevel) => {
                            const attribute = this.formContext.getAttribute(fieldName);
                            if (!attribute) {
                                console.error(`Attribute ${fieldName} not found.`);
                                return;
                            }
                            attribute.setRequiredLevel(requiredLevel);
                        },
                        getLookupValue: (fieldName) => {
                            const attribute = this.formContext.getAttribute(fieldName);
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
                    this.formContext = _formContext;
                }
                async callCustomApi(apiName, data) {
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
                        const responseData = await response.json();
                        console.log('Custom API Response: ', responseData);
                        return responseData;
                    }
                    catch (error) {
                        console.log('Error Calling the custom Api: ', error);
                        throw error;
                    }
                }
            }
            Common.CommonUtil = CommonUtil;
        })(Common = OSIRIS.Common || (OSIRIS.Common = {}));
    })(OSIRIS = SW365.OSIRIS || (SW365.OSIRIS = {}));
})(SW365 || (SW365 = {}));
