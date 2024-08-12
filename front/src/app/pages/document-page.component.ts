import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DocumentsService } from '../../services/documents-service';
import { map, Observable, of } from 'rxjs';
import { DocumentDocument } from '../../services/generated';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-document-page',
  standalone: true,
  imports: [AsyncPipe],
  template: ` @if(document$ | async; as document){
    <div>{{ document.name }}</div>
    }`,
  styles: '',
})
export class DocumentPageComponent implements OnInit {
  private _activatedRoute = inject(ActivatedRoute);
  private _documentsService = inject(DocumentsService);

  public document$: Observable<DocumentDocument | null> = of(null);

  ngOnInit(): void {
    let documentId = this._activatedRoute.snapshot.params['id'];
    this.document$ = this._documentsService
      .getDocument(documentId)
      .pipe(map((result) => <DocumentDocument | null>result.data.document));
  }
}
