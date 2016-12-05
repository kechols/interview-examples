package com.medfusion.interview.client;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

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
		List<DocumentReference> results = getOnlyCcdDocuments();
		
		if(results.isEmpty()) {
			LoggerFactory.getLogger(this.getClass().getName()).debug("The document was not found.");
			return null;
		}
		
		Collections.sort(results, new Comparator<DocumentReference>(){
		   @Override
		   public int compare(final DocumentReference leftHandSideDocument, DocumentReference rightHandSideDocument){
		     return compareDocumentReferecesByDate(rightHandSideDocument, leftHandSideDocument);
		     }
		 });

		LoggerFactory.getLogger(this.getClass().getName()).info(String.format("Document type is %1$s.", searchParams.get(DOCUMENT_TYPE)), results.get(0));
		String documentId = results.get(0).getId();
		LoggerFactory.getLogger(this.getClass().getName()).debug("Found the document {}", documentId);
		return documentId;
		
	}
	

	/**
	 * <p>
	 * Gets the number of CCD Documents.
	 * </p>
	 * 
	 * @return
	 */
	public long getCcdDocumentCount() {
		return getOnlyCcdDocuments().size();
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
	
	
	private int compareDocumentReferecesByDate (DocumentReference rightHandSideDocument, DocumentReference leftHandSideDocument) {
		long rightHandSideDocumentDateTime = 0;
		long  leftHandSideDocumentDateTime = 0;
		
		try {
			rightHandSideDocumentDateTime = dateFormatter.parse(rightHandSideDocument.getCreated()).getTime();
			leftHandSideDocumentDateTime = dateFormatter.parse(leftHandSideDocument.getCreated()).getTime();
		} catch (ParseException e) {
			throw new RuntimeException (e);
		}
		
		return rightHandSideDocumentDateTime < leftHandSideDocumentDateTime ? -1 : rightHandSideDocumentDateTime == leftHandSideDocumentDateTime ? 0 : 1;
    }
	
	
	private List<DocumentReference> getEmptyDocumentReferencList () {
		return new ArrayList<DocumentReference>();
    }
	
	
	private List<DocumentReference> getOnlyCcdDocuments () {
		searchParams = new HashMap<String, String>();
		searchParams.put(DOCUMENT_TYPE, "CCD");

		List<DocumentReference> providerService = getProviderService().search(searchParams);
		List<DocumentReference> results = getEmptyDocumentReferencList();
		
		for (DocumentReference documentReference : providerService) {
			if (documentReference.getType().equals(searchParams.get(DOCUMENT_TYPE))){
				results.add(documentReference);
			}
		}
		
		return results;
    }
	
	
	private void loadDoNothingProviderService () {
        this.providerService = new ProviderService() {
			@Override
			public List<DocumentReference> search(Map<String, String> searchParams) {
				return getEmptyDocumentReferencList();
			}
		};
    }
}
