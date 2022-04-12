﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace VamsDeviceListener.wsEvents {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="wsDeviceEventsSoap", Namespace="http://tempuri.org/")]
    public partial class wsDeviceEvents : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetApiDetailsOperationCompleted;
        
        private System.Threading.SendOrPostCallback PushRealTimeEventsOperationCompleted;
        
        private System.Threading.SendOrPostCallback PushDisplayMessageEventsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public wsDeviceEvents() {
            this.Url = global::VamsDeviceListener.Properties.Settings.Default.VamsDeviceListener_wsEvents_wsDeviceEvents;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetApiDetailsCompletedEventHandler GetApiDetailsCompleted;
        
        /// <remarks/>
        public event PushRealTimeEventsCompletedEventHandler PushRealTimeEventsCompleted;
        
        /// <remarks/>
        public event PushDisplayMessageEventsCompletedEventHandler PushDisplayMessageEventsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetApiDetails", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public sdkCallEventList GetApiDetails() {
            object[] results = this.Invoke("GetApiDetails", new object[0]);
            return ((sdkCallEventList)(results[0]));
        }
        
        /// <remarks/>
        public void GetApiDetailsAsync() {
            this.GetApiDetailsAsync(null);
        }
        
        /// <remarks/>
        public void GetApiDetailsAsync(object userState) {
            if ((this.GetApiDetailsOperationCompleted == null)) {
                this.GetApiDetailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetApiDetailsOperationCompleted);
            }
            this.InvokeAsync("GetApiDetails", new object[0], this.GetApiDetailsOperationCompleted, userState);
        }
        
        private void OnGetApiDetailsOperationCompleted(object arg) {
            if ((this.GetApiDetailsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetApiDetailsCompleted(this, new GetApiDetailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PushRealTimeEvents", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool PushRealTimeEvents(sdkBSEventsRealTimeData zkRTD) {
            object[] results = this.Invoke("PushRealTimeEvents", new object[] {
                        zkRTD});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void PushRealTimeEventsAsync(sdkBSEventsRealTimeData zkRTD) {
            this.PushRealTimeEventsAsync(zkRTD, null);
        }
        
        /// <remarks/>
        public void PushRealTimeEventsAsync(sdkBSEventsRealTimeData zkRTD, object userState) {
            if ((this.PushRealTimeEventsOperationCompleted == null)) {
                this.PushRealTimeEventsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPushRealTimeEventsOperationCompleted);
            }
            this.InvokeAsync("PushRealTimeEvents", new object[] {
                        zkRTD}, this.PushRealTimeEventsOperationCompleted, userState);
        }
        
        private void OnPushRealTimeEventsOperationCompleted(object arg) {
            if ((this.PushRealTimeEventsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PushRealTimeEventsCompleted(this, new PushRealTimeEventsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/PushDisplayMessageEvents", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool PushDisplayMessageEvents(string message) {
            object[] results = this.Invoke("PushDisplayMessageEvents", new object[] {
                        message});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void PushDisplayMessageEventsAsync(string message) {
            this.PushDisplayMessageEventsAsync(message, null);
        }
        
        /// <remarks/>
        public void PushDisplayMessageEventsAsync(string message, object userState) {
            if ((this.PushDisplayMessageEventsOperationCompleted == null)) {
                this.PushDisplayMessageEventsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnPushDisplayMessageEventsOperationCompleted);
            }
            this.InvokeAsync("PushDisplayMessageEvents", new object[] {
                        message}, this.PushDisplayMessageEventsOperationCompleted, userState);
        }
        
        private void OnPushDisplayMessageEventsOperationCompleted(object arg) {
            if ((this.PushDisplayMessageEventsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.PushDisplayMessageEventsCompleted(this, new PushDisplayMessageEventsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class sdkCallEventList {
        
        private sdkEventsApiDetails[] zkApiDetListField;
        
        /// <remarks/>
        public sdkEventsApiDetails[] zkApiDetList {
            get {
                return this.zkApiDetListField;
            }
            set {
                this.zkApiDetListField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class sdkEventsApiDetails {
        
        private string apiURLField;
        
        private string apiUserNameField;
        
        private string apiPasswordField;
        
        private string apiTKNField;
        
        private string apiUniqueIdField;
        
        private string apiIsEntryEventRequiredField;
        
        private string apiIsElevatorEventRequiredField;
        
        private string apiTimezoneNameField;
        
        private sdkProcessFor sdkEventForField;
        
        /// <remarks/>
        public string apiURL {
            get {
                return this.apiURLField;
            }
            set {
                this.apiURLField = value;
            }
        }
        
        /// <remarks/>
        public string apiUserName {
            get {
                return this.apiUserNameField;
            }
            set {
                this.apiUserNameField = value;
            }
        }
        
        /// <remarks/>
        public string apiPassword {
            get {
                return this.apiPasswordField;
            }
            set {
                this.apiPasswordField = value;
            }
        }
        
        /// <remarks/>
        public string apiTKN {
            get {
                return this.apiTKNField;
            }
            set {
                this.apiTKNField = value;
            }
        }
        
        /// <remarks/>
        public string apiUniqueId {
            get {
                return this.apiUniqueIdField;
            }
            set {
                this.apiUniqueIdField = value;
            }
        }
        
        /// <remarks/>
        public string apiIsEntryEventRequired {
            get {
                return this.apiIsEntryEventRequiredField;
            }
            set {
                this.apiIsEntryEventRequiredField = value;
            }
        }
        
        /// <remarks/>
        public string apiIsElevatorEventRequired {
            get {
                return this.apiIsElevatorEventRequiredField;
            }
            set {
                this.apiIsElevatorEventRequiredField = value;
            }
        }
        
        /// <remarks/>
        public string apiTimezoneName {
            get {
                return this.apiTimezoneNameField;
            }
            set {
                this.apiTimezoneNameField = value;
            }
        }
        
        /// <remarks/>
        public sdkProcessFor sdkEventFor {
            get {
                return this.sdkEventForField;
            }
            set {
                this.sdkEventForField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum sdkProcessFor {
        
        /// <remarks/>
        FRSAFARAPI,
        
        /// <remarks/>
        ZKBIOPACKAPI,
        
        /// <remarks/>
        ZKBIOSECURITYAPI,
        
        /// <remarks/>
        GALLAGHERAPI,
        
        /// <remarks/>
        BIOSTAR2API,
        
        /// <remarks/>
        SIPASSAPI,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class sdkBSEventsRealTimeData {
        
        private string idField;
        
        private string eventTimeField;
        
        private string pinField;
        
        private string nameField;
        
        private string lastNameField;
        
        private string deptNameField;
        
        private string areaNameField;
        
        private string cardNoField;
        
        private string devSnField;
        
        private string verifyModeNameField;
        
        private string eventNameField;
        
        private string eventPointNameField;
        
        private string readerNameField;
        
        private string accZoneField;
        
        private string devNameField;
        
        private string logIdField;
        
        private string eventNumberField;
        
        private string photoPathField;
        
        private string visitorPhotoField;
        
        private string maskFlagField;
        
        private string temperatureField;
        
        private string statusField;
        
        private string readerStatusField;
        
        private string eventResultField;
        
        private string eventFromField;
        
        private string inoutStatusField;
        
        private string uniqueIdField;
        
        private string eventScannedFromField;
        
        /// <remarks/>
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        public string eventTime {
            get {
                return this.eventTimeField;
            }
            set {
                this.eventTimeField = value;
            }
        }
        
        /// <remarks/>
        public string pin {
            get {
                return this.pinField;
            }
            set {
                this.pinField = value;
            }
        }
        
        /// <remarks/>
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        public string lastName {
            get {
                return this.lastNameField;
            }
            set {
                this.lastNameField = value;
            }
        }
        
        /// <remarks/>
        public string deptName {
            get {
                return this.deptNameField;
            }
            set {
                this.deptNameField = value;
            }
        }
        
        /// <remarks/>
        public string areaName {
            get {
                return this.areaNameField;
            }
            set {
                this.areaNameField = value;
            }
        }
        
        /// <remarks/>
        public string cardNo {
            get {
                return this.cardNoField;
            }
            set {
                this.cardNoField = value;
            }
        }
        
        /// <remarks/>
        public string devSn {
            get {
                return this.devSnField;
            }
            set {
                this.devSnField = value;
            }
        }
        
        /// <remarks/>
        public string verifyModeName {
            get {
                return this.verifyModeNameField;
            }
            set {
                this.verifyModeNameField = value;
            }
        }
        
        /// <remarks/>
        public string eventName {
            get {
                return this.eventNameField;
            }
            set {
                this.eventNameField = value;
            }
        }
        
        /// <remarks/>
        public string eventPointName {
            get {
                return this.eventPointNameField;
            }
            set {
                this.eventPointNameField = value;
            }
        }
        
        /// <remarks/>
        public string readerName {
            get {
                return this.readerNameField;
            }
            set {
                this.readerNameField = value;
            }
        }
        
        /// <remarks/>
        public string accZone {
            get {
                return this.accZoneField;
            }
            set {
                this.accZoneField = value;
            }
        }
        
        /// <remarks/>
        public string devName {
            get {
                return this.devNameField;
            }
            set {
                this.devNameField = value;
            }
        }
        
        /// <remarks/>
        public string logId {
            get {
                return this.logIdField;
            }
            set {
                this.logIdField = value;
            }
        }
        
        /// <remarks/>
        public string eventNumber {
            get {
                return this.eventNumberField;
            }
            set {
                this.eventNumberField = value;
            }
        }
        
        /// <remarks/>
        public string photoPath {
            get {
                return this.photoPathField;
            }
            set {
                this.photoPathField = value;
            }
        }
        
        /// <remarks/>
        public string visitorPhoto {
            get {
                return this.visitorPhotoField;
            }
            set {
                this.visitorPhotoField = value;
            }
        }
        
        /// <remarks/>
        public string maskFlag {
            get {
                return this.maskFlagField;
            }
            set {
                this.maskFlagField = value;
            }
        }
        
        /// <remarks/>
        public string temperature {
            get {
                return this.temperatureField;
            }
            set {
                this.temperatureField = value;
            }
        }
        
        /// <remarks/>
        public string status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }
        
        /// <remarks/>
        public string readerStatus {
            get {
                return this.readerStatusField;
            }
            set {
                this.readerStatusField = value;
            }
        }
        
        /// <remarks/>
        public string eventResult {
            get {
                return this.eventResultField;
            }
            set {
                this.eventResultField = value;
            }
        }
        
        /// <remarks/>
        public string eventFrom {
            get {
                return this.eventFromField;
            }
            set {
                this.eventFromField = value;
            }
        }
        
        /// <remarks/>
        public string inoutStatus {
            get {
                return this.inoutStatusField;
            }
            set {
                this.inoutStatusField = value;
            }
        }
        
        /// <remarks/>
        public string uniqueId {
            get {
                return this.uniqueIdField;
            }
            set {
                this.uniqueIdField = value;
            }
        }
        
        /// <remarks/>
        public string eventScannedFrom {
            get {
                return this.eventScannedFromField;
            }
            set {
                this.eventScannedFromField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void GetApiDetailsCompletedEventHandler(object sender, GetApiDetailsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetApiDetailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetApiDetailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public sdkCallEventList Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((sdkCallEventList)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void PushRealTimeEventsCompletedEventHandler(object sender, PushRealTimeEventsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PushRealTimeEventsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PushRealTimeEventsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    public delegate void PushDisplayMessageEventsCompletedEventHandler(object sender, PushDisplayMessageEventsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.4084.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class PushDisplayMessageEventsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal PushDisplayMessageEventsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591