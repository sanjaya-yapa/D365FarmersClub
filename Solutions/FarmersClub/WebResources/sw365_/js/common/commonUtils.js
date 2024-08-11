var SW365;
(function (SW365) {
    var OSIRIS;
    (function (OSIRIS) {
        var Common;
        (function (Common) {
            class CommonUtil {
                constructor(_formContext) {
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
