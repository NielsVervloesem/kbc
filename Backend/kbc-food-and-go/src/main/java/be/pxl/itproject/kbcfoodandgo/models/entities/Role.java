package be.pxl.itproject.kbcfoodandgo.models.entities;

public enum Role {
    CUSTOMER("CUSTOMER"), ADMIN("ADMIN"),CAFETARIA_EMPLOYEE("CAFETARIA_EMPLOYEE");

    private String value;

    Role(String value) { this.value = value; }
    public String getValue() { return value; }
}
