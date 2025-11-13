import { Injectable } from '@angular/core';
import { ApiHttpService } from './../../../shared/services/api-http.service'
import { Observable } from 'rxjs';

@Injectable()
export class RoleService {
   
    

    constructor(private apiHttpService: ApiHttpService) {

    }

    getRoleList() {

        let url = 'system-settings/roles/get-active-role-list';
        return this.apiHttpService.GET(url);
    }

    getRole(id: string)
    {
        let url = 'system-settings/roles/get-role?id=' + id;;
        return this.apiHttpService.GET(url);
    }

    createRole(data: any)
    {
        let url = 'system-settings/roles/create-role';
        return this.apiHttpService.POST(url, data);
    }

    updateRole(id: any, data: any) {
        let url = 'system-settings/roles/update-role/' + id;
        return this.apiHttpService.PUT(url, data);
    }

    deleteEntity(id: any) {
        let url = 'system-settings/roles/delete-custom-category/' + id;
        return this.apiHttpService.DELETE(url);
    }

    deleteEntities(ids: any[]) {
        let url = 'system-settings/roles/delete-custom-categories/';
        return this.apiHttpService.POST(url, ids);
    }

    getRoleTreeListSvcUrlByBusinessProfileAsync(businessProfileId: number) {
        let url = 'system-settings/roles/get-role-tree-list-by-bp/' + businessProfileId ;
        return url;
    }

    getRoleTreeListSvcUrlForCopyOptionAsync(businessProfileId: number, roleId: string) {
        let url = 'system-settings/roles/get-role-tree-list-for-copy-option/' + businessProfileId + '/' + roleId;
        return url;
    }

    getUnassignUserListAsync(businessProfileId: number, roleID: string) {
        let url = 'system-settings/roles/unassign-user-list/' + businessProfileId + '/' + roleID;
        return url;
    }

    getUnassignUserGridColumns(): any {
        return [
            {
                "dataField": "name",
                "caption": "Name"
            }];
    }

   

    getUserByRoleList(roleId: string, params: any)
    {
        let url = 'system-settings/roles/get-user-role-list/' + roleId;
        return this.apiHttpService.GETGridData(url, params);
    }
    
    getUserPermissionList(parentRoleId: string)
    {
        let url = 'system-settings/roles/get-user-permission-list/' + parentRoleId;
        return this.apiHttpService.GET(url);
    }

    getPermissionListByRole(ids: any[]) {        
        let url = 'system-settings/roles/get-role-right-list';
        return this.apiHttpService.POST(url, ids);
    }

    assignRoleToUsers(roleId: string, userIds: string[]): any {
        let url = 'system-settings/roles/assign-role-to-users/' + roleId;
        
        return this.apiHttpService.POST(url, userIds);
    }
    removeUserFromRole(roleId: string, userId: string) {
        let url = 'system-settings/roles/remove-user-from-role/' + roleId + '/' + userId;        
        return this.apiHttpService.DELETE(url);
    }
}
