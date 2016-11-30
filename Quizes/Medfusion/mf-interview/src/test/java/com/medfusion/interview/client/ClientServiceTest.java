package com.medfusion.interview.client;

import static org.junit.Assert.assertEquals;

import java.text.ParseException;
import java.util.Arrays;
import java.util.List;
import java.util.Map;

import org.junit.Assert;
import org.junit.Test;

import com.medfusion.interview.data.DocumentReference;
import com.medfusion.interview.service.ProviderService;

public class ClientServiceTest {
	
	private static final String EMPTY_STRING = "";
	private static final String EXPECTED_EXCEPTION = "Expected runtime exception";
	private static final String INVALID_DOCUMENT_TYPE = "Blah";
	private static final String UNEXPECTED_EXCEPTION_OCCURRED = "An unexpected exception occurred : %1$s";
	
	@Test
    public void shouldReturnId_GettingCurrentCcdDocumentId_For_ValidDocument() {
		ClientService clientService = new ClientService();
		
		final String expectedId = "kte";
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return Arrays.asList(
					ClientServiceTestFixture.Instance().createDocument(
						expectedId, 
						searchParams.get(ClientService.DOCUMENT_TYPE), 
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
    public void shouldReturnLatestId_GettingCurrentCcdDocumentId_For_ValidDocuments() {
		ClientService clientService = new ClientService();
		
		final String expectedId = "latest";
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				ClientServiceTestFixture testFixture = ClientServiceTestFixture.Instance();
				return Arrays.asList(
					testFixture.createDocument(
						"early1", 
						searchParams.get(ClientService.DOCUMENT_TYPE), 
						ClientService.dateFormatter.format(ClientServiceTestFixture.dateTimeHoursBack(1))
					),
					testFixture.createDocument(
						expectedId, 
						searchParams.get(ClientService.DOCUMENT_TYPE), 
						ClientService.dateFormatter.format(ClientServiceTestFixture.dateTimeNow())
					),
					testFixture.createDocument(
						"early2", 
						searchParams.get(ClientService.DOCUMENT_TYPE), 
						ClientService.dateFormatter.format(ClientServiceTestFixture.dateTimeHoursBack(2))
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
    public void shouldReturnNull_GettingCurrentCcdDocumentId_For_InValidDocumentType() {
		ClientService clientService = new ClientService();
		
		final String expectedId = null;
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return Arrays.asList(
					ClientServiceTestFixture.Instance().createDocument(
						expectedId, 
						INVALID_DOCUMENT_TYPE, 
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
	
	
	@Test
    public void shouldReturnCorrectCount_GetttingCcdDocumentCount() {
		ClientService clientService = new ClientService();
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				ClientServiceTestFixture testFixture = ClientServiceTestFixture.Instance();
				return Arrays.asList( // searchParams.get(ClientService.DOCUMENT_TYPE),
					testFixture.createDocument(
						"1", 
						searchParams.get(ClientService.DOCUMENT_TYPE), 
						EMPTY_STRING
					),
					testFixture.createDocument(
						"2", 
						INVALID_DOCUMENT_TYPE, 
						EMPTY_STRING
					),
					testFixture.createDocument(
						"3", 
						searchParams.get(ClientService.DOCUMENT_TYPE), 
						EMPTY_STRING
					)
				);
			}
		});
		
		try {
			Long actualCount = clientService.getCcdDocumentCount();
			assertEquals(new Long(2), actualCount);
		}
		catch(Exception ex){
			Assert.fail(String.format(UNEXPECTED_EXCEPTION_OCCURRED, ex.getMessage()));
		}
    }
	
	
	@Test
    public void shouldReturnZero_GetttingCcdDocumentCount_After_Constructing() {
		ClientService clientService = new ClientService();
		
		try {
			Long actualCount = clientService.getCcdDocumentCount();
			assertEquals(new Long(0), actualCount);
		}
		catch(Exception ex){
			Assert.fail(String.format(UNEXPECTED_EXCEPTION_OCCURRED, ex.getMessage()));
		}
    }
	
	
	@Test
    public void shouldReturnZero_GetttingCcdDocumentCount_For_InValidDocumentType() {
		ClientService clientService = new ClientService();
		
		final String expectedId = null;
		
		clientService.setProviderService(new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return Arrays.asList(
					ClientServiceTestFixture.Instance().createDocument(
						expectedId, 
						INVALID_DOCUMENT_TYPE, 
						""
					)
				);
			}
		});
		
		try {
			Long actualCount = clientService.getCcdDocumentCount();
			assertEquals(new Long(0), actualCount);
		}
		catch(Exception ex){
			Assert.fail(String.format(UNEXPECTED_EXCEPTION_OCCURRED, ex.getMessage()));
		}
    }
}
