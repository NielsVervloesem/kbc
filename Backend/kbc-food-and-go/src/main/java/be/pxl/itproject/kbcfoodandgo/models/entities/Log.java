package be.pxl.itproject.kbcfoodandgo.models.entities;
import com.fasterxml.jackson.annotation.JsonFormat;
import javax.persistence.*;
import java.util.Date;

@Entity
@Table(name = "Logs")
public class Log {
    @Id
    @GeneratedValue(strategy = GenerationType.AUTO)
    private long id;

    private String message;
    @JsonFormat(pattern = "dd-MM-yyyy HH:mm:ss")
    private Date date;

    public Log() {
    }

    public Log(String message, Date logTime) {
        this.message = message;
        this.date = logTime;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getMessage() {
        return message;
    }

    public void setMessage(String message) {
        this.message = message;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public static class Builder {
        private long id;
        private String message;
        private Date date;

        public Builder(long id){
            this.id = id;
        }

        public Builder withMessage(String message){
            this.message = message;
            return this;
        }

        public Builder atDate(Date date){
            this.date = date;
            return this;
        }

        public Log build(){
            Log log = new Log();
            log.id = this.id;
            log.date = this.date;
            log.message = this.message;
            return log;
        }
    }
}
