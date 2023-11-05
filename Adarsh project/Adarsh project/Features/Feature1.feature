Feature: Dynamic HTML TABLE Tag

Here we are working with selemium test to validate table data with input data

@tag1
Scenario: Dynamic html table Tag with chrome browser
	Given I navigate to the launch browaer
	And I navigated to the Demo web page
	And I click on Table Data button for text box
	When I enter the given data in the text box
	And I click on refresh button
	Then entered data should be populated in the table
	