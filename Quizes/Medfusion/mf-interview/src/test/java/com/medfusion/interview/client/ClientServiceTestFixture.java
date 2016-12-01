package com.medfusion.interview.client;

import java.util.Calendar;
import java.util.Date;

import com.medfusion.interview.data.DocumentReference;

public class ClientServiceTestFixture {
	private DocumentReference testDocument = null;
	
	private ClientServiceTestFixture (){}
	
	public static Date dateTimeNow () {
		return new Date();
	}
	
	public static Date dateTimeHoursBack (int hoursBack) {
		Calendar calendar = Calendar.getInstance();
		calendar.setTime(dateTimeNow());
		calendar.add(Calendar.HOUR,  -hoursBack);
		return calendar.getTime();
	}
	
	
	public static ClientServiceTestFixture Instance(){
		return new ClientServiceTestFixture();
	}
	
	
	public ClientServiceTestFixture createDocument(){
		testDocument = new DocumentReference();
		return this;
	}
	
	
	public DocumentReference createDocument(String id, String type, String created){
		createDocument().withId(id).withType(type).created(created);
		return testDocument;
	}
	
	
	public ClientServiceTestFixture withId(String id){
		createIfNecessary();
		testDocument.setId(id);
		return this;
	}
	
	
	public ClientServiceTestFixture withType(String type){
		createIfNecessary();
		testDocument.setType(type);
		return this;
	}
	
	
	public ClientServiceTestFixture created(String created){
		createIfNecessary();
		testDocument.setCreated(created);
		return this;
	}
	
	
	private void createIfNecessary(){
		if (testDocument == null){
			createDocument();
		}
	}
}
