import { Injectable } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import {
  AllDocumentsCollectionSegment,
  AllDocumentsGQL,
  AllDocumentsQuery,
  AllDocumentsQueryVariables,
  DocumentGQL,
  DocumentQuery,
} from '../graphql/generated';
import { map, Observable, Subject, tap } from 'rxjs';
import { QueryRef } from 'apollo-angular';

@Injectable({
  providedIn: 'root',
})
export class DocumentsService {
  private _allDocumentsQueryRef: QueryRef<
    AllDocumentsQuery,
    AllDocumentsQueryVariables
  >;

  public allDocuments$: Observable<AllDocumentsCollectionSegment>;

  public allDocumentsLoading$: Subject<boolean> = new Subject<boolean>();

  constructor(
    private _documentsGQL: AllDocumentsGQL,
    private _documentGQL: DocumentGQL
  ) {
    this._allDocumentsQueryRef = this._documentsGQL.watch({
      skip: 0,
      take: 15,
    });

    this.allDocuments$ = this._allDocumentsQueryRef.valueChanges.pipe(
      tap(() => this.allDocumentsLoading$.next(false)),
      map((result) => <AllDocumentsCollectionSegment>result.data.allDocuments)
    );
  }

  public refreshDocuments(skip: number, take: number): void {
    this.allDocumentsLoading$.next(true);
    this._allDocumentsQueryRef.refetch({
      skip: skip,
      take: take,
    });
  }

  public getDocument(id: string): Observable<ApolloQueryResult<DocumentQuery>> {
    return this._documentGQL.watch({ id }).valueChanges;
  }
}
