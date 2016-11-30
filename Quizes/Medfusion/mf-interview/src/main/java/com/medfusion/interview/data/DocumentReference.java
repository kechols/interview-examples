package com.medfusion.interview.data;

public class DocumentReference {

	private String id;
	private String type;
	private String created;

	public String getId() {
		return this.id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getType() {
		return this.type;
	}

	public void setType(String type) {
		this.type = type;
	}

	/**
	 * <p>
	 * A date, date-time or partial date (e.g. just year or year + month) as used in human communication. If hours and
	 * minutes are specified, a time zone SHALL be populated. Seconds may be provided but may also be ignored. Dates
	 * SHALL be valid dates.
	 * </p>
	 * <p>
	 * dateTime is a union of the w3c schema types dateTime, date, gYearMonth, gYear
	 * </p>
	 * <p>
	 * regex:-?([1-9][0-9]{3}|0[0-9]{3})(-(0[1-9]|1[0-2])(-(0[1-9]|[12][0-9]|3[01])(T(([01][0-9]|2[0-3]):[0-5][0-9]:[0-5
	 * ][0-9](\.[0-9]+)?|(24:00:00(\.0+)?))(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?)?)?)?
	 * </p>
	 * <p>
	 * Note: not all restrictions imposed on this type can be expressed using regular expressions, so this regex still
	 * allows a broader set of values than allowed by FHIR
	 * </p>
	 */
	public String getCreated() {
		return this.created;
	}

	public void setCreated(String created) {
		this.created = created;
	}

}
