import { CommonModule, NgFor } from '@angular/common';
import { Component, input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DataViewModule } from 'primeng/dataview';
import { TagModule } from 'primeng/tag';
import { Document } from '../../graphql/generated';
import { DocumentListItemComponent } from './document-list-item.component';

@Component({
  selector: 'app-document-list',
  standalone: true,
  imports: [
    DataViewModule,
    NgFor,
    CommonModule,
    TagModule,
    ButtonModule,
    DocumentListItemComponent,
  ],
  template: `<div class="card">
    <p-dataView
      #dv
      [value]="documents()"
      [rows]="totalCount()"
      [rowsPerPageOptions]="[15]"
      [paginator]="true"
    >
      <ng-template pTemplate="list" let-documents>
        <div class="grid grid-nogutter">
          <app-document-list-item
            *ngFor="let document of documents; let first = first"
            [document]="document"
            [first]="first"
            class="col-12"
          >
          </app-document-list-item>
        </div>
      </ng-template>
    </p-dataView>
  </div>`,
  styles: '',
})
export class DocumentListComponent {
  documents = input<Array<Document>>();
  totalCount = input<number>();
}
