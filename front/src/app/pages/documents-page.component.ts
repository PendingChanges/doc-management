import { Component, inject, OnInit } from '@angular/core';
import { DocumentListComponent } from '../organisms/document-list.component';
import { DocumentsService } from '../../services/documents-service';
import { AsyncPipe } from '@angular/common';
import { DocumentUploadButtonComponent } from '../organisms/document-upload-button.component';

@Component({
  selector: 'app-documents-page',
  standalone: true,
  imports: [DocumentListComponent, AsyncPipe, DocumentUploadButtonComponent],
  template: ` <app-document-upload-button></app-document-upload-button>
    <app-document-list
      [documents]="(allDocuments$ | async)?.items || []"
      [totalCount]="(allDocuments$ | async)?.totalCount || 0"
    ></app-document-list>`,
  styles: '',
})
export class DocumentsPageComponent implements OnInit {
  private _documentService = inject(DocumentsService);
  public allDocuments$ = this._documentService.allDocuments$;

  ngOnInit() {
    this._documentService.refreshDocuments(0, 15);
  }
}
