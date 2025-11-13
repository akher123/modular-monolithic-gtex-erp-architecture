import { Component, AfterViewInit, ViewChild, Inject, AfterContentChecked } from '@angular/core';
import { DxSelectBoxModule, DxTextAreaModule, DxDateBoxModule, DxFormModule, DxNumberBoxModule, DxTextBoxModule, DxCheckBoxModule, DxValidatorModule, DxValidationGroupComponent, DxColorBoxComponent, DxTreeListComponent, DxTreeListModule, DxFileUploaderComponent, DxDataGridComponent, DxValidatorComponent, DxTreeViewComponent } from 'devextreme-angular';
import { MessageService } from './../../../../shared/services/message.service'
import { NavigationService } from './../../../../shared/services/navigation.service'
import { TitlebarComponent } from './../../../application-shared/components/titlebar/titlebar.component';
import { ImageUploadComponent } from './../../../application-shared/components/common/image-upload.component';
import { ActivatedRoute, Router, Params } from '@angular/router';
import { ToolbarType, DetailPageAction } from './../../../application-shared/components/titlebar/utilities';
import { retry } from 'rxjs/operators';
import { RoleService } from './../../../system-settings/services/role.service';
import DataSource from 'devextreme/data/data_source';
import { HttpClient, HttpClientModule, HttpParams } from "@angular/common/http";
import { ToolbarItem, ToolbarItemOption } from '../../../application-shared/components/titlebar/toolbar-item';
import CustomStore from "devextreme/data/custom_store";
import "rxjs/add/operator/toPromise";
import { TreeViewDropDownBoxComponent } from './../../../application-shared/components/common/app-tree-view-drop-down-box.component';
import { EntitySelectBoxComponent } from './../../../application-shared/components/common/app-entity-select-box.component';
import { alert } from 'devextreme/ui/dialog';
declare var $: any;

@Component({
    selector: 'app-role-form',
    templateUrl: './role-form.component.html',
    styleUrls: ['./role-form.component.scss']
})
/** role-form component*/
export class RoleFormComponent implements AfterViewInit, AfterContentChecked {


    role: any = {};
    emptyRole: any = {};
    roles: any = [];
    userDataSource: any = [];
    rightDataSource: any = [];
    roleId: string = "";
    businessProfileSelectItems: any = [];
    selectedRows: any[];
    roleSelectItems: any = [];
    titleSelectItems: any = [];
    isDefaultBusinessProfileUser = false;
    disabledInUpdateMode = false;
    disabledBusinessProfile = false;
    treeListRoleInstance = undefined;

    toolbarAdditionalItems: ToolbarItem[];
    contentClass: any = "detail-page-content-div";
    userDetailPageUrl = 'system-settings/user/';

    userDivClass: string = "col-sm-12 col-xl-4 col-lg-12 div-two-column-right";
    hideUserDivClass: string = "col-sm-12 col-xl-4 col-lg-12 div-two-column-right item-invisible"
    showUserDivClass: string = "col-sm-12 col-xl-4 col-lg-12 div-two-column-right";

    @ViewChild(TitlebarComponent)
    private titlebar: TitlebarComponent;

    @ViewChild('grdUser')
    grdUser: DxDataGridComponent;


    @ViewChild(DxTreeListComponent)
    rightTreeList: DxTreeListComponent;

    @ViewChild('parentRoleDropDown')
    parentRoleDropDown: TreeViewDropDownBoxComponent

    @ViewChild('copyExistingRoleDropDown')
    copyExistingRoleDropDown: TreeViewDropDownBoxComponent

    @ViewChild('userEntitySelectionBox')
    private userEntitySelectionBox: EntitySelectBoxComponent;

    @ViewChild('formValidation')
    private formValidation: DxValidationGroupComponent;

    @ViewChild('businessProfileValidation')
    private businessProfileValidation: DxValidatorComponent;

    //@ViewChild('parentRoleValidation')
    //private parentRoleValidation: DxValidatorComponent;

    @ViewChild('roleNameValidation')
    private roleNameValidation: DxValidatorComponent;

    constructor(private roleService: RoleService
        , private messageService: MessageService
        , private navigationService: NavigationService
        , private route: ActivatedRoute
        , private router: Router) {
        this.route.params.subscribe(params => {
            if (params['roleId'] != undefined) {
                this.roleId = params['roleId'];
            }
        });

        this.toolbarAdditionalItems = [];

    }
   

    /*
     * add additional menu item
     **/
    addAdditionalToolbar() {

        //expand menu
        var expandItem = new ToolbarItem();
        expandItem.location = 'after';
        expandItem.widget = 'dxButton';
        expandItem.locateInMenu = 'auto';
        expandItem.visible = true;
        expandItem.disabled = false;

        var expandItemOption = new ToolbarItemOption();
        expandItemOption.icon = 'chevrondown';
        expandItemOption.text = 'Expand';
        expandItemOption.onClick = () => {
            this.onExpandClicked();
        };
        expandItem.options = expandItemOption;
        this.toolbarAdditionalItems.push(expandItem);

        //collapse menu
        var collapseItem = new ToolbarItem();
        collapseItem.location = 'after';
        collapseItem.widget = 'dxButton';
        collapseItem.locateInMenu = 'auto';
        collapseItem.visible = true;
        collapseItem.disabled = false;

        var collapseItemOption = new ToolbarItemOption();
        collapseItemOption.icon = 'chevronup';
        collapseItemOption.text = 'Collapse';
        collapseItemOption.onClick = () => {
            this.onCollapseClicked();
        };
        collapseItem.options = collapseItemOption;
        this.toolbarAdditionalItems.push(collapseItem);

    }



    public get isNewRole(): boolean {
        return this.roleId == "" || this.roleId == undefined || this.roleId == null;
    }


    ngAfterViewInit(): void {
        // this.init()
        this.attachValidationToControl();



    }

    ngAfterContentChecked(): void {
        this.disabledBusinessProfile = !this.isNewRole || !this.isDefaultBusinessProfileUser;;
        
        this.disabledInUpdateMode = !this.isNewRole; 

        this.userDivClass = this.isNewRole ? this.hideUserDivClass : this.showUserDivClass;
    }

    ngOnInit(): void {
        this.addAdditionalToolbar();

        this.init();
        this.userEntitySelectionBox.setColumns(this.roleService.getUnassignUserGridColumns());
    }

    init(): void {
        this.roleService.getRole(this.roleId).subscribe(data => {

            this.role = data.result.role;
            this.emptyRole = data.result.emptyRole;
            this.businessProfileSelectItems = data.result.businessProfileSelectItems;            
            this.isDefaultBusinessProfileUser = data.result.isDefaultBusinessProfile;

            if (!this.isNewRole) {
                this.loadRightDataSource(this.role.parentRoleId);
                this.userEntitySelectionBox.setDataSource(this.roleService.getUnassignUserListAsync(this.role.businessProfileId, this.roleId));
            }

            this.titlebar.initializeToolbar((this.roleId == undefined || this.roleId == null || this.roleId == "") ? "Role: New " :
                "Role : " + data.result.role.name, this.toolbarAdditionalItems, ToolbarType.DetailPage);
        });

        if (this.roleId !== undefined && this.roleId !== null && this.roleId !== "") {
            this.userDataSource = new DataSource({
                load: (loadOptions: any) => {
                    return this.roleService.getUserByRoleList(this.roleId, loadOptions).toPromise().then((response: any) => {
                        return {
                            data: response.result.data,
                            totalCount: response.result.totalCount
                        }
                    });
                }
            });
        }
    }
    onBusinessProfileValueChanged(e): void {
        
        this.parentRoleDropDown.setDatasource(this.roleService.getRoleTreeListSvcUrlByBusinessProfileAsync(e.value));
    }
    /*
    * on expand clicked
    **/
    onExpandClicked() {

        var keys: any[] = [];

        this.treeListRoleInstance.forEachNode(function (node) {
            keys.push(node.key);
        });

        for (var index in keys) {
            this.treeListRoleInstance.expandRow(keys[index]);
        }
    }

    /*
     * on collapse clicked
     **/
    onCollapseClicked() {

        var keys: any[] = [];
        this.treeListRoleInstance.forEachNode(function (node) {
            keys.push(node.key);
        });

        for (var index in keys) {
            this.treeListRoleInstance.collapseRow(keys[index]);
        }
    }

    /**
    * Event
    * @param e
    */
    onRoleTreelistInitialized(e) {

        this.treeListRoleInstance = e.component;
    }

    /**
     * attach validation to the controls
     *
     * */
    attachValidationToControl() {

        //validation        
        this.businessProfileValidation.validationRules = [{ type: 'required', message: 'Business Profile is required.' }];
        //this.parentRoleValidation.validationRules = [{ type: 'required', message: 'Parent Role is required.' }];
        this.roleNameValidation.validationRules = [{ type: 'required', message: 'Role Name is required.' }];
    }

    /**
     * on value changed event
     * @param e
     */
    onParentRoleSelectedValueChange(e) {

        if (this.roleId == undefined || this.roleId == null || this.roleId == "") {
            this.copyExistingRoleDropDown.setDatasource(this.roleService.getRoleTreeListSvcUrlForCopyOptionAsync(this.role.businessProfileId, e));
            this.loadRightDataSource(e);
        }
  }

  /**
   * on value changed event
   * @param e
   */
  onCopyRoleSelectedValueChange(e) {
    var copyRoleId = e.selectedValue;
    if (copyRoleId == undefined || copyRoleId == null || copyRoleId == "") {
     // this.copyExistingRoleDropDown.setDatasource(this.roleService.getRoleTreeListSvcUrlForCopyOptionAsync(this.role.businessProfileId, copyRoleId));
      this.loadRightDataSource(e);
    }
  }
  
    onParentRoleClearButtonClick() {
        this.copyExistingRoleDropDown.clearDatasource();
    }

    assignRoleToUsers(): void {        
        let ids = this.userEntitySelectionBox.selectedIds;
        if (ids != null) {
            
            this.roleService.assignRoleToUsers(this.roleId, ids).subscribe(data => {
                if (data.result) {
                    this.messageService.showAssignUsersToRoleMsg(ids.length);
                    this.userEntitySelectionBox.setDataSource(this.roleService.getUnassignUserListAsync(this.role.businessProfileId, this.roleId));
                    this.grdUser.instance.getDataSource().reload();
                }  
            });
        }
        else {
            this.messageService.showInvalidSelectionMsg();
        }
    }

    removeUserFromRole(data: any): void {
       
        var result = this.messageService.showRemoveUserFromRoleConfirmMsg(data.key.userName);
            result.then(dialogResult => {
                if (dialogResult) {
                    this.roleService.removeUserFromRole(this.roleId, data.key.id).subscribe(data => {
                        this.messageService.showRemoveUserFromRoleMsg();
                        this.userEntitySelectionBox.selectedIds = [];
                        this.userEntitySelectionBox.setDataSource(this.roleService.getUnassignUserListAsync(this.role.businessProfileId, this.roleId));
                        this.grdUser.instance.getDataSource().reload();
                    });
                }
            });
    }
    
    /**
     * load right data source based on roleid
     * @param parentRoleId
     */
    loadRightDataSource(parentRoleId: any): void {
        
        this.roleService.getUserPermissionList(parentRoleId).subscribe(data => {
            this.rightDataSource = data.result;
        });
    }

    getSelectedRowKeys() {
        return this.rightTreeList.instance.getSelectedRowKeys("leavesOnly"); // or "excludeRecursive" | "leavesOnly"
    }

    /**
     * validate and save data
     */
    validateAndSave(action: DetailPageAction): void {

        if (!this.formValidation.instance.validate().isValid) {
            return;
        }

        this.saveEntity(action);
    }

    /**
     * 
     * @param closedWindow
     * @param isNew
     */
    saveEntity(action: DetailPageAction): void {

        this.role.parentRoleId = this.parentRoleDropDown.seletedValue;

        if (this.roleId !== undefined && this.roleId !== null && this.roleId !== "") {
            this.role.roleRights = this.rightTreeList.instance.getSelectedRowKeys("leavesOnly");
            this.roleService.updateRole(this.roleId, this.role).subscribe(data => {
                this.messageService.success("Record has been update successfully", 'Information');
                this.redirectToListPage(action);
            });
        }
        else {
            this.role.copyRoleId = this.copyExistingRoleDropDown.seletedValue;
            this.roleService.createRole(this.role).subscribe(data => {
                this.messageService.success("Record has been save successfully", 'Information');
                this.roleId = data.result;
                this.redirectToListPage(action);
            });
        }
    }

    /**
    * on save button clicked
    */
    onSaveClicked(e): void {
        this.validateAndSave(DetailPageAction.Save);
    }

    /**
     * on save and new button clicked
     */
    onSaveNNewClicked(): void {
        this.validateAndSave(DetailPageAction.SaveAndNew);
    }

    /**
     * on save and close button clicked
     */
    onSaveNCloseClicked(): void {

        this.validateAndSave(DetailPageAction.SaveAndClose);
    }
    /**
     * on close button clicked
     */
    onCloseClicked(): void {
        this.redirectToListPage(DetailPageAction.Close);
    }

    /**
    * redirect to list page
    */
    redirectToListPage(action: DetailPageAction): void {
        var newNavigationUrl = '/system-settings/role';

        if (action == DetailPageAction.Close || action == DetailPageAction.SaveAndClose) {

            this.navigationService.navigateToReturnurl(this.router.url);
        }
        else if (action == DetailPageAction.SaveAndNew) {

            this.role = JSON.parse(JSON.stringify(this.emptyRole));
            this.roleId = null;
            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl, this.router.url);
        }
        else if (action == DetailPageAction.Save && newNavigationUrl == this.router.url) {

            this.navigationService.navigateAndUpdateReturnUrl(newNavigationUrl + '/' + this.roleId, this.router.url);
        }
        else {
            this.titlebar.setToolbarTitle("Role : " + this.role.name);
        }
    }
}
