package com.medfusion.interview.client;

import java.text.DateFormat;
import java.text.ParseException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.Date;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Map;
import java.util.Vector;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.medfusion.interview.data.DocumentReference;
import com.medfusion.interview.service.ProviderService;

public class ClientService {

	private static String DOCUMENT_TYPE;
	private static String DOCUMENT_PERIOD_BEFORE;
	private static String DOCUMENT_PERIOD_AFTER;

	public ProviderService providerService;

	private HashMap<String, String> searchParams;

	public ClientService() {
		DOCUMENT_TYPE = "type";
		DOCUMENT_PERIOD_BEFORE = "periodBefore";
		DOCUMENT_PERIOD_AFTER = "periodAfter";
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

		DocumentReference[] results = providerService.search(searchParams).toArray(new DocumentReference[0]);

		Enumeration<DocumentReference> docRefEnum = new Vector(providerService.search(searchParams)).elements();

		System.out.print("here...");
		int i = 0;
		Date current = null;
		while (docRefEnum.hasMoreElements()) {
			DocumentReference d = docRefEnum.nextElement();

			try {
				final Date dd = DateFormat.getInstance().parse(d.getCreated());

				if (current == null) {
					current = dd;
				}

				if (dd.getTime() > current.getTime()) {
					current = dd;
					i++;
				} else if (dd.getTime() == current.getTime()) {
					current = dd;
					i++;
				} else if (dd.getTime() < current.getTime()) {
					i++;
				}
			} catch (Exception e) {
				e.printStackTrace();
			}
		}

		System.out.print("here...");
		String documentId = null;
		boolean found = false;
		if (results[i].getType() == "CCD") {
			LoggerFactory.getLogger(this.getClass().getName()).info("Document type is CCD.", results[0]);
			documentId = results[0].getId();
			found = true;
		}

		System.out.print("here...");
		if (found = true) {
			LoggerFactory.getLogger(this.getClass().getName()).debug("Found the document {}", documentId);
			return documentId;
		} else {
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

		Enumeration<DocumentReference> docRefEnum = new Vector(providerService.search(searchParams)).elements();

		int count = 0;
		while (docRefEnum.hasMoreElements()) {
			DocumentReference d = docRefEnum.nextElement();
			if (d.getType() == "CCD" && d != null) {
				count++;
			}
		}

		return count;

	}

	public ProviderService getProviderService() {
		return this.providerService;
	}

	public void setProviderService(ProviderService providerService) {
		this.providerService = providerService;
	}

}
