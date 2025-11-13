import { Component, Input } from '@angular/core';
import { dynamicPageService } from '../services/dynamic-page.service';
import { MessageService } from '../../shared/services/message.service';
import { NavigationService } from '../../shared/services/navigation.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-list-page-detail',
    templateUrl: './list-page-detail.component.html',
    styleUrls: ['./list-page-detail.component.scss']
})

export class ListPageDetailComponent {
    listPageId: number = 114
    pageRoutingUrl: any = '';
    pageSchemaSource: any=[];

    entityDataSource: any = {};
    emptyEntityDataSource: any = {};
    componentDataSource: any = {};

    @Input('PageComponentKey') pageComponentKey: any;

    constructor(private listPageService: dynamicPageService,
        //private route: ActivatedRoute,
        private router: Router,
        private messageService: MessageService,
        private navigationService: NavigationService) {

        this.pageRoutingUrl = this.router.url;

    }

    /**
     * ngOnInit event
     */
    ngOnInit() {
        this.getPageSchema();
    }

    /*
   * set page information
   */
    getPageSchema(): void {

        if (this.pageComponentKey != null && this.pageComponentKey != undefined) {
            this.listPageService.getPageInfoByName(this.pageComponentKey)
                .subscribe(data => {
                    this.setPageProperty(data);
                    //this.listTitlebar.initializeToolbar(this.listPage.title, this.toolbarAdditionalItems, ToolbarType.ListTabPage);
                    //this.contentClass = "detail-page-content-div-tab";
                    //this.toolbarType = ToolbarType.ListTabPage;
                    //this.gridHeight = window.innerHeight - this.listTabGridHeightToReduce;
                });
        }
        else {
            this.listPageService.getPageInfoByRoutingUrl(this.pageRoutingUrl)
                .subscribe(data => {
                    this.setPageProperty(data);
                    //this.listTitlebar.initializeToolbar(this.listPage.title, this.toolbarAdditionalItems, ToolbarType.ListPage);
                    //this.toolbarType = ToolbarType.ListPage;
                    //this.gridHeight = window.innerHeight - this.listGridHeightToReduce;
                });
        }
    }

    private setPageProperty(data: any) {
        this.pageSchemaSource = data.result;
        this.entityDataSource.name = '';
        this.entityDataSource.routingUrl = '';
        this.entityDataSource.pageType = '';

        this.getListPageDetails();


        //this.columns = this.listPage.fields;
        //this.setServiceUrl();
        //this.setGridDataSource();
        //this.setNavigationUrl();
    }

    private getListPageDetails() {
        this.listPageService.getListPageInfoById(this.listPageId)
            .subscribe(data => {
                this.entityDataSource = data.result.applicationPage;
                this.emptyEntityDataSource = data.result.emptyApplicationPage;
                this.componentDataSource.pageTypeDataSource = data.result.pageTypeDataSource;
            });
    }

}
