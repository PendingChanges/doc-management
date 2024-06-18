Feature: Document

A short summary of the feature

@document
Scenario: The user create a document
	Given No existing document
	When A user with id "<userid>" create a document with key "<key>" name "<documentName>" and extension "<extension>"
	Then A document with name "<documentName>", extension "<extension" is created by "<userid>"
	
Examples:
	| key | userid   | documentName | extension |
	| key | testuser | MyDocument   | ext       |

@document
Scenario: A user delete a document
	Given An existing document with key "<key>", name "<name>" and extension "<extension>"
	When A user delete the document
	Then The document is deleted
	And No errors

Examples:
	| key | documentName | extension |
	| key | MyDocument   | ext       |