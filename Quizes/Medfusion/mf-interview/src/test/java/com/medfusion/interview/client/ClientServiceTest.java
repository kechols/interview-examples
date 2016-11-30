package com.medfusion.interview.client;

import static org.junit.Assert.assertEquals;

import java.text.DateFormat;
import java.text.ParseException;
import java.util.Arrays;
import java.util.Calendar;
import java.util.List;
import java.util.Map;

import org.junit.Assert;
import org.junit.Test;

import com.medfusion.interview.data.DocumentReference;
import com.medfusion.interview.service.ProviderService;

public class ClientServiceTest {
	
	private static final String EXPECTED_EXCEPTION = "Expected runtime exception";
	private static final String UNEXPECTED_EXCEPTION_OCCURRED = "An unexpected exception occurred : %1$s";
	
	@Test
    public void shouldReturnId_GettingCurrentCcdDocumentId_For_ValidDocument() {
		ClientService clientService = new ClientService();
		
		final String expectedId = "kte";
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				ClientServiceTestFixture testFixture = ClientServiceTestFixture.Instance();
				return Arrays.asList(
						ClientServiceTestFixture.Instance().createDocument(
							expectedId, 
							"CCD", 
							ClientService.dateFormatter.format(ClientServiceTestFixture.dateTimeNow())
						)
				);
			}
		});
		
		try {
			String actualId = clientService.getCurrentCcdDocumentId();
			assertEquals(expectedId, actualId);
		}
		catch(Exception ex){
			Assert.fail(String.format(UNEXPECTED_EXCEPTION_OCCURRED, ex.getMessage()));
		}
    }
	
	
	@Test
    public void shouldReturnNull_GettingCurrentCcdDocumentId_After_Constructing() {
		ClientService clientService = new ClientService();
		
		try {
			String actualId = clientService.getCurrentCcdDocumentId();
			assertEquals(null, actualId);
		}
		catch(Exception ex){
			Assert.fail(String.format(UNEXPECTED_EXCEPTION_OCCURRED, ex.getMessage()));
		}
    }
	
	
	@Test
    public void shouldThrowException_GettingCurrentCcdDocumentId_For_InvalidCreatedDate() {
		ClientService clientService = new ClientService();
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return Arrays.asList(
						ClientServiceTestFixture.Instance().createDocument("kte", "kevins", "InvalidCreatedDate")
				);
			}
		});
		
		try {
			clientService.getCurrentCcdDocumentId();
			Assert.fail(EXPECTED_EXCEPTION);
		}
		catch(Exception actual){
			assertEquals(ParseException.class, actual.getClass());
		}
    }
	
	
	@Test
    public void shouldThrowException_GettingCurrentCcdDocumentId_When_ThereIsProblem() {
		ClientService clientService = new ClientService();
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return null;
			}
		});
		
		try {
			clientService.getCurrentCcdDocumentId();
			Assert.fail(EXPECTED_EXCEPTION);
		}
		catch(Exception actual){
			assertEquals(NullPointerException.class, actual.getClass());
		}
    }

}
