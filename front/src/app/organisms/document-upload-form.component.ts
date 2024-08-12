import { Component, inject, model } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FileUploadEvent, FileUploadModule } from 'primeng/fileupload';
import { MessageService } from 'primeng/api';
import { NgFor, NgIf } from '@angular/common';
import { environment } from '../../infrastructure/environments/environment.development';
import { EndpointsService } from '../../services/generated';
import { DocumentsService } from '../../services/documents-service';

@Component({
  selector: 'app-document-upload-form',
  standalone: true,
  imports: [ButtonModule, InputTextModule, FileUploadModule, NgIf, NgFor],
  template: `<p-fileUpload
    name="files[]"
    [url]="uploadUrl"
    (onUpload)="onUpload($event)"
    [multiple]="true"
    accept="image/*,application/pdf"
    maxFileSize="1000000"
  >
  </p-fileUpload>`,
  styles: '',
})
export class DocumentUploadFormComponent {
  private _documentService = inject(DocumentsService);
  visible = model<boolean>();
  uploadUrl = environment.apiUrl + '/documents';

  confirm() {
    this.visible.update((v) => false);
  }

  cancel() {
    this.visible.update((v) => false);
  }

  constructor() {}

  onUpload(event: FileUploadEvent) {
    this._documentService.refreshDocuments(0, 15);
    this.confirm();
  }
}
