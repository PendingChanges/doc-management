import { gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** The `Short` scalar type represents non-fractional signed whole 16-bit numeric values. Short can represent values between -(2^15) and 2^15 - 1. */
  Short: { input: any; output: any; }
  UUID: { input: any; output: any; }
};

/** A segment of a collection. */
export type AllDocumentsCollectionSegment = {
  __typename?: 'AllDocumentsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Document>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export enum ApplyPolicy {
  AfterResolver = 'AFTER_RESOLVER',
  BeforeResolver = 'BEFORE_RESOLVER',
  Validation = 'VALIDATION'
}

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
};

export type Document = {
  __typename?: 'Document';
  extension: Scalars['String']['output'];
  fileNameWithoutExtension: Scalars['String']['output'];
  id: Scalars['UUID']['output'];
  name: Scalars['String']['output'];
  version: Version;
};

export type Query = {
  __typename?: 'Query';
  allDocuments?: Maybe<AllDocumentsCollectionSegment>;
  document?: Maybe<Document>;
};


export type QueryAllDocumentsArgs = {
  skip?: InputMaybe<Scalars['Int']['input']>;
  sortBy?: InputMaybe<Scalars['String']['input']>;
  sortDirection?: InputMaybe<Scalars['String']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
};


export type QueryDocumentArgs = {
  id: Scalars['UUID']['input'];
  version?: InputMaybe<Scalars['String']['input']>;
};

export type Version = {
  __typename?: 'Version';
  build: Scalars['Int']['output'];
  major: Scalars['Int']['output'];
  majorRevision: Scalars['Short']['output'];
  minor: Scalars['Int']['output'];
  minorRevision: Scalars['Short']['output'];
  revision: Scalars['Int']['output'];
};

export type AllDocumentsQueryVariables = Exact<{
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
}>;


export type AllDocumentsQuery = { __typename?: 'Query', allDocuments?: { __typename?: 'AllDocumentsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Document', extension: string, fileNameWithoutExtension: string, id: any, name: string, version: { __typename?: 'Version', major: number, minor: number } }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type DocumentQueryVariables = Exact<{
  id: Scalars['UUID']['input'];
  version?: InputMaybe<Scalars['String']['input']>;
}>;


export type DocumentQuery = { __typename?: 'Query', document?: { __typename?: 'Document', extension: string, fileNameWithoutExtension: string, id: any, name: string, version: { __typename?: 'Version', major: number, minor: number } } | null };

export const AllDocumentsDocument = gql`
    query allDocuments($skip: Int, $take: Int) {
  allDocuments(skip: $skip, take: $take) {
    items {
      extension
      fileNameWithoutExtension
      id
      name
      version {
        major
        minor
      }
    }
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
    totalCount
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class AllDocumentsGQL extends Apollo.Query<AllDocumentsQuery, AllDocumentsQueryVariables> {
    document = AllDocumentsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const DocumentDocument = gql`
    query document($id: UUID!, $version: String) {
  document(id: $id, version: $version) {
    extension
    fileNameWithoutExtension
    id
    name
    version {
      major
      minor
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class DocumentGQL extends Apollo.Query<DocumentQuery, DocumentQueryVariables> {
    document = DocumentDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }