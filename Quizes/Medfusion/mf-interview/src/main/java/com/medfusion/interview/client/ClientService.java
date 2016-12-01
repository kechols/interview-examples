package com.medfusion.interview.client;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Vector;

import org.slf4j.LoggerFactory;

import com.medfusion.interview.data.DocumentReference;
import com.medfusion.interview.service.ProviderService;

public class ClientService {

	static final String DOCUMENT_TYPE = "type";

	
	public final static SimpleDateFormat dateFormatter = new SimpleDateFormat("dd/MM/yy HH:mm");

	public ProviderService providerService;

	private HashMap<String, String> searchParams;

	public ClientService() {
	}

	/**
	 * <p>
	 * Gets the Id of the most recent CCD Document.
	 * </p>
	 * 
	 * @return The id of the most recent CCD Document or null.
	 * @throws Exception
	 *             When there is a problem.
	 */
	public String getCurrentCcdDocumentId() throws Exception {

		searchParams = new HashMap<String, String>();
		searchParams.put(DOCUMENT_TYPE, "CCD");

		List<DocumentReference> results = getProviderService().search(searchParams);

		Date currentDocumentReferenceDateTime = null;
		DocumentReference result = null;
		for (DocumentReference documentReference : results) {
			final Date documentReferenceDateTime = dateFormatter.parse(documentReference.getCreated());

			if ((currentDocumentReferenceDateTime == null || 
				documentReferenceDateTime.getTime() >= currentDocumentReferenceDateTime.getTime()) && 
				documentReference.getType().equals(searchParams.get(DOCUMENT_TYPE))	
				) {
				currentDocumentReferenceDateTime = documentReferenceDateTime;
				result = documentReference;
			}
		}
		
		if (result != null){
			LoggerFactory.getLogger(this.getClass().getName()).info(String.format("Document type is %1$s.", searchParams.get(DOCUMENT_TYPE)), result);
			String documentId = result.getId();
			LoggerFactory.getLogger(this.getClass().getName()).debug("Found the document {}", documentId);
			return documentId;
		}
		else {
			LoggerFactory.getLogger(this.getClass().getName()).debug("The document was not found.");
			return null;
		}
	}
	

	/**
	 * <p>
	 * Gets the number of CCD Documents.
	 * </p>
	 * 
	 * @return
	 */
	public long getCcdDocumentCount() {
		searchParams = new HashMap<String, String>();
		searchParams.put(DOCUMENT_TYPE, "CCD");

		List<DocumentReference> results = getProviderService().search(searchParams);

		int count = 0;
		
		for (DocumentReference documentReference : results) {
			if (documentReference.getType().equals(searchParams.get(DOCUMENT_TYPE))){
				count++;
			}
		}

		return count;
	}
	

	public ProviderService getProviderService() {
		if(this.providerService == null){
			loadDoNothingProviderService();
		}
		return this.providerService;
	}
	

	public void setProviderService(ProviderService providerService) {
		this.providerService = providerService;
	}
	
	
	private void loadDoNothingProviderService () {
        this.providerService = new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return new ArrayList<DocumentReference>();
			}
		};
    }
}