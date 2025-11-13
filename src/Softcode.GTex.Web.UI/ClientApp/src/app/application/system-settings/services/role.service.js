"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var RoleService = /** @class */ (function () {
    function RoleService(apiHttpService) {
        this.apiHttpService = apiHttpService;
    }
    RoleService.prototype.getRoleList = function () {
        var url = 'system-settings/roles/get-active-role-list';
        return this.apiHttpService.GET(url);
    };
    RoleService.prototype.getRole = function (id) {
        var url = 'system-settings/roles/get-role?id=' + id;
        ;
        return this.apiHttpService.GET(url);
    };
    RoleService.prototype.createRole = function (data) {
        var url = 'system-settings/roles/create-role';
        return this.apiHttpService.POST(url, data);
    };
    RoleService.prototype.updateRole = function (id, data) {
        var url = 'system-settings/roles/update-role/' + id;
        return this.apiHttpService.PUT(url, data);
    };
    RoleService.prototype.deleteEntity = function (id) {
        var url = 'system-settings/roles/delete-custom-category/' + id;
        return this.apiHttpService.DELETE(url);
    };
    RoleService.prototype.deleteEntities = function (ids) {
        var url = 'system-settings/roles/delete-custom-categories/';
        return this.apiHttpService.POST(url, ids);
    };
    RoleService.prototype.getRoleTreeListSvcUrlByBusinessProfileAsync = function (businessProfileId) {
        var url = 'system-settings/roles/get-role-tree-list-by-bp/' + businessProfileId;
        return url;
    };
    RoleService.prototype.getUnassignUserListAsync = function (businessProfileId, roleID) {
        var url = 'system-settings/roles/unassign-user-list/' + businessProfileId + '/' + roleID;
        return url;
    };
    RoleService.prototype.getUnassignUserGridColumns = function () {
        return [
            {
                "dataField": "name",
                "caption": "Name"
            }
        ];
    };
    RoleService.prototype.getRoleTreeListSvcUrlByBPIdAndRoleIdAsync = function (businessProfileId, roleId) {
        var url = 'system-settings/roles/get-role-tree-list-by-bp/' + businessProfileId + '/' + roleId;
        return url;
    };
    RoleService.prototype.getUserByRoleList = function (roleId, params) {
        var url = 'system-settings/roles/get-user-role-list/' + roleId;
        return this.apiHttpService.GETGridData(url, params);
    };
    RoleService.prototype.getUserPermissionList = function (parentRoleId) {
        var url = 'system-settings/roles/get-user-permission-list/' + parentRoleId;
        return this.apiHttpService.GET(url);
    };
    RoleService.prototype.getPermissionListByRole = function (ids) {
        var url = 'system-settings/roles/get-role-right-list';
        return this.apiHttpService.POST(url, ids);
    };
    RoleService.prototype.assignRoleToUsers = function (roleId, userIds) {
        var url = 'system-settings/roles/assign-role-to-users/' + roleId;
        return this.apiHttpService.POST(url, userIds);
    };
    RoleService.prototype.removeUserFromRole = function (roleId, userId) {
        var url = 'system-settings/roles/remove-user-from-role/' + roleId + '/' + userId;
        return this.apiHttpService.DELETE(url);
    };
    RoleService = __decorate([
        core_1.Injectable()
    ], RoleService);
    return RoleService;
}());
exports.RoleService = RoleService;
//# sourceMappingURL=role.service.js.map