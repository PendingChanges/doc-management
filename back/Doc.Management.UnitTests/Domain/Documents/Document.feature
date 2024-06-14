Feature: Document

A short summary of the feature

@document
Scenario: The user create a document
	Given No existing document
	When A user with id "<userid>" create a document with name "<documentName>"
	Then A document "<documentName>" created by "<userid>" is created
	
Examples:
	| userid   | documentName |
	| testuser | MyDocument   |

@document
Scenario: A user delete a document
	Given An existing document with name "<documentName>"
	When A user delete the document
	Then The document is deleted
	And No errors

Examples:
	| documentName  |
	| MyDocument |