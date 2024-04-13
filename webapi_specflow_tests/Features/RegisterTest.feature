Feature: Users can register on the application
	So that they can use the application

	Scenario: User of type DisabilityExpert can register
		Given the user sends a request to register
		When the user is of type DisabilityExpertWithoutGuardian
		When all the required fields are filled
		Then the user is registered

	Scenario: User of type Company can register
		Given the user sends a request to register
		When the user is of type CompanyApproved
		When all the required fields are filled
		Then the user is registered

	Scenario: User of type Guardian can register
		Given the user sends a request to register
		When the user is of type Guardian
		When all the required fields are filled
		Then the user is registered
