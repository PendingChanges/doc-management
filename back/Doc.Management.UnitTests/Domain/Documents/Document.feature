Feature: Document

A short summary of the feature

@document
Scenario: The user create a document
	Given No existing document
	When A user with id "<userid>" create a document with key "<key>" name "<documentName>", filename "<filename>" and extension "<extension>"
	Then A document with name "<documentName>", filnemae "<filename>" extension "<extension>" is created by "<userid>"
	
Examples:
	| key | userid   | documentName | filename | extension |
	| 2024/06/26/kdakkozakokdoza | testuser | MyDocument   | file     | ext       |

@document
Scenario: A user delete a document
	Given An existing document with key "<key>", name "<name>", filename "<filename>" and extension "<extension>"
	When A user delete the document
	Then The document is deleted
	And No errors

Examples:
	| key | documentName | filename | extension |
	| 2024/06/26/kdakkozakokdoza | MyDocument   | file     | ext       |

@document
Scenario: A user modifies a document
  Given An existing document with key "<key>", name "<name>", filename "<filename>" and extension "<extension>"
  When A user with id "<userid>" modifies the document with new name "<newName>", new filename "<newFilename>", and new extension "<newExtension>"
  Then The document's name, filename, and extension are updated to "<newName>", "<newFilename>", and "<newExtension>" respectively

Examples:
  | key | userid   | name       | filename | extension | newName    | newFilename | newExtension |
  | 2024/06/26/kdakkozakokdoza | testuser | MyDocument | file      | ext        | MyUpdatedDoc | updatedfile | newext      |