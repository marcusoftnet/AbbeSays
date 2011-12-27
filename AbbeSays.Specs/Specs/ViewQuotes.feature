@ignore
Feature: Viewing quotes
	In order to keep track of what different kids has said
	As a user of the site
	I want to see the quotes the kids has made during the years

Background: 
	Given the following quotes in the database
			| Kid    | Quote   | Said at    |
			| Albert | Quote 1 | 2011-01-01 |
			| Albert | Quote 2 | 2011-01-02 |
			| Arvid  | Quote 3 | 2011-10-01 |
			| Gustav | Quote 4 | 2011-11-01 |
	
Scenario: Viewing quotes for Albert
	When I navigate to the quotes page
	Then I should see the following quotes:
			| Kid    | Quote   | Said at    |
			| Albert | Quote 1 | 2011-01-01 |
			| Albert | Quote 2 | 2011-01-02 |
			| Arvid  | Quote 3 | 2011-10-01 |
			| Gustav | Quote 4 | 2011-11-01 |
		And I should see a list of the following kids:
			| Name   |
			| Albert |
			| Arvid  |
			| Gustav | 