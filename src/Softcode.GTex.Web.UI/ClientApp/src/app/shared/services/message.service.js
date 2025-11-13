"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var dialog_1 = require("devextreme/ui/dialog");
var MessageService = /** @class */ (function () {
    function MessageService(toastrService) {
        this.toastrService = toastrService;
    }
    MessageService.prototype.showDataSavedMsg = function () {
        this.toastrService.success(AppMessage.DataDelete, 'Information');
    };
    MessageService.prototype.showDataUpdatedMsg = function () {
        this.toastrService.success(AppMessage.DataUpdated, 'Information');
    };
    MessageService.prototype.showDataDeletedMsg = function () {
        this.toastrService.success(AppMessage.DataDelete, 'Information');
    };
    MessageService.prototype.showInvalidSelectionMsg = function () {
        this.toastrService.warning(AppMessage.InvalidSelection, 'Invalid Selection');
    };
    MessageService.prototype.showAssignUsersToRoleMsg = function (userCount) {
        if (userCount == 1) {
            this.toastrService.success(AppMessage.AssignUserToRole, 'Information');
        }
        else if (userCount > 1) {
            this.toastrService.success(AppMessage.AssignUsersToRole, 'Information');
        }
    };
    MessageService.prototype.showDeleteConfirmMsg = function (recordCount) {
        var re = /~!Param1!~/gi;
        var msg = AppMessage.DeleteConfirm.toString().replace(re, recordCount);
        return dialog_1.confirm(msg, "Delete Confirmation");
    };
    MessageService.prototype.showRemoveUserFromRoleConfirmMsg = function (userName) {
        var re = /~!Param1!~/gi;
        var msg = AppMessage.RemoveUserFromRole.toString().replace(re, userName);
        return dialog_1.confirm(msg, "Remove Confirmation");
    };
    MessageService.prototype.showInvalidServiceUrlMsg = function (serviceName) {
        var re = /~!Param1!~/gi;
        var msg = AppMessage.InvalidServiceUrl.toString().replace(re, serviceName);
        this.toastrService.warning(msg, 'Invalid Configuration');
    };
    MessageService.prototype.showInvalidNavigationUrlMsg = function (navigationLink) {
        var re = /~!Param1!~/gi;
        var msg = AppMessage.InvalidNavigationUrl.toString().replace(re, navigationLink);
        this.toastrService.warning(msg, 'Invalid Configuration');
    };
    MessageService.prototype.success = function (message, header) {
        this.toastrService.success(message, header, {});
    };
    MessageService.prototype.error = function (message, header) {
        this.toastrService.error(message, header, {
            disableTimeOut: true,
        });
    };
    MessageService.prototype.warning = function (message, header) {
        this.toastrService.warning(message, header, {});
    };
    MessageService.prototype.info = function (message, header) {
        this.toastrService.info(message, header, {});
    };
    MessageService.prototype.show = function (message, header) {
        this.toastrService.show(message, header, {});
    };
    MessageService = __decorate([
        core_1.Injectable()
    ], MessageService);
    return MessageService;
}());
exports.MessageService = MessageService;
var AppMessage;
(function (AppMessage) {
    AppMessage["DataSaved"] = "Record(s) has been saved successfully";
    AppMessage["DataUpdated"] = "Record(s) has been saved successfully";
    AppMessage["DataDelete"] = "Record(s) has been deleted successfully";
    AppMessage["InvalidSelection"] = "Please select one or more records from the list and try again";
    AppMessage["DeleteConfirm"] = "You are about to delete ~!Param1!~ record(s). This process can not be reverted. Do you want to continue?";
    AppMessage["RemoveUserFromRole"] = "You are about to remove user: ~!Param1!~ from this role. This process can not be reverted. Do you want to continue?";
    AppMessage["InvalidServiceUrl"] = "Please configure Service Url for ~Param1~";
    AppMessage["InvalidNavigationUrl"] = "Please configure Navigation Url for ~!Param1!~";
    AppMessage["InvalidDocuemtUpload"] = "Please select one or more files and try again";
    AppMessage["AssignUserToRole"] = "One user is successfully assigned to this role";
    AppMessage["AssignUsersToRole"] = "Few users are successfully assigned to this role";
})(AppMessage = exports.AppMessage || (exports.AppMessage = {}));
//# sourceMappingURL=message.service.js.map