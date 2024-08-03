import { Component, model } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FileUploadEvent, FileUploadModule } from 'primeng/fileupload';
import { MessageService } from 'primeng/api';
import { NgFor, NgIf } from '@angular/common';
import { environment } from '../../infrastructure/environments/environment.development';

@Component({
  selector: 'app-document-upload-form',
  standalone: true,
  imports: [ButtonModule, InputTextModule, FileUploadModule, NgIf, NgFor],
  providers: [MessageService],
  template: ` <p-fileUpload
    name="demo[]"
    [url]="uploadUrl"
    (onUpload)="onUpload($event)"
    [multiple]="true"
    accept="image/*,application/pdf"
    maxFileSize="1000000"
  >
    <ng-template pTemplate="content">
      <ul *ngIf="uploadedFiles.length">
        <li *ngFor="let file of uploadedFiles">
          {{ file.name }} - {{ file.size }} bytes
        </li>
      </ul>
    </ng-template>
  </p-fileUpload>`,
  styles: '',
})
export class DocumentUploadFormComponent {
  visible = model<boolean>();
  uploadUrl = environment.apiUrl + '/documents';

  confirm() {
    this.visible.update((v) => false);
  }

  cancel() {
    this.visible.update((v) => false);
  }
  uploadedFiles: any[] = [];

  constructor(private messageService: MessageService) {}

  onUpload(event: FileUploadEvent) {
    for (let file of event.files) {
      this.uploadedFiles.push(file);
    }

    this.messageService.add({
      severity: 'info',
      summary: 'File Uploaded',
      detail: '',
    });
  }
}
