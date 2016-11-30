package com.medfusion.interview.client;

import java.util.List;
import java.util.Map;

import static org.junit.Assert.*;

import org.junit.Assert;
import org.junit.Test;

import com.medfusion.interview.data.DocumentReference;
import com.medfusion.interview.service.ProviderService;

public class ClientServiceTest {
	
	private static final String EXPECTED_EXCEPTION = "Expected runtime exception";
	private static final String UNEXPECTED_EXCEPTION_OCCURRED = "An unexpected exception occurred : %1$s";
	
	@Test
    public void shouldReturnNull_GettingCurrentCcdDocumentId_AfterConstructing() {
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
    public void shouldThrowException_GettingCurrentCcdDocumentId_WhenThereIsProblem() {
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
