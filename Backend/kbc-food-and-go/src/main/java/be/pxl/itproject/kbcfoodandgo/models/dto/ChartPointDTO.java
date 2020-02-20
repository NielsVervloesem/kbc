package be.pxl.itproject.kbcfoodandgo.models.dto;

public class ChartPointDTO {
    private String label;
    private double value;

    public ChartPointDTO(String label, double value) {
        this.label = label;
        this.value = value;
    }

    public ChartPointDTO() {
    }

    public String getLabel() {
        return label;
    }

    public void setLabel(String label) {
        this.label = label;
    }

    public double getValue() {
        return value;
    }

    public void setValue(double value) {
        this.value = value;
    }
}
