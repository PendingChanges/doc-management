import { CommonModule, NgFor } from '@angular/common';
import { Component, inject, input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DataViewLazyLoadEvent, DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { Document } from '../../graphql/generated';
import { DocumentListItemComponent } from './document-list-item.component';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { PaginatorModule } from 'primeng/paginator';
import { DocumentsService } from '../../services/documents-service';
import { SkeletonModule } from 'primeng/skeleton';
import { DocumentListItemSkeletonComponent } from './document-list-item-skeleton.component';

@Component({
  selector: 'app-document-list',
  standalone: true,
  imports: [
    DataViewModule,
    PaginatorModule,
    NgFor,
    ToastModule,
    CommonModule,
    TagModule,
    ButtonModule,
    SkeletonModule,
    DocumentListItemComponent,
    DocumentListItemSkeletonComponent,
  ],
  providers: [MessageService],
  template: `<div class="card">
    <p-toast />
    <p-dataView
      #dv
      [value]="documents()"
      [rows]="15"
      [first]="0"
      [totalRecords]="totalCount()"
      [paginator]="true"
      [lazy]="true"
      [paginatorPosition]="'both'"
      (onLazyLoad)="onLazyLoad($event)"
    >
      <ng-template pTemplate="list" let-documents>
        <div class="grid grid-nogutter">
          @if(_documentService.allDocumentsLoading$ | async) {
          <app-document-list-item-skeleton
            *ngFor="let i of counterArray(6); let first = first"
            [first]="first"
          ></app-document-list-item-skeleton>
          } @else {
          <app-document-list-item
            *ngFor="let document of documents; let first = first"
            [document]="document"
            [first]="first"
            class="col-12"
          >
          </app-document-list-item>

          }
        </div>
      </ng-template>
    </p-dataView>
  </div>`,
  styles: '',
})
export class DocumentListComponent {
  public _documentService = inject(DocumentsService);

  documents = input<Array<Document>>();
  first: number = 0;
  rows: number = 15;
  totalCount = input<number>();
  onLazyLoad(event: DataViewLazyLoadEvent) {
    this._documentService.refreshDocuments(event.first, event.rows);
  }

  counterArray(n: number): any[] {
    return Array(n);
  }
}
