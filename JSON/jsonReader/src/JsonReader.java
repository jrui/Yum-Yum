import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import java.io.FileReader;
import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.Map;
import java.util.TreeMap;
import org.json.simple.parser.ParseException;

public class JsonReader {
    public static Map<String, Restaurant> rests = new TreeMap<>();
    public static ArrayList<String> cousineTypes = new ArrayList<>();

    public static void main(String[] args) throws IOException, ParseException, SQLException {
        processaDados();  //Ja valida, comentarios e cozinha (retira N/A's)
        verificaDuplicados();  //Remove entradas duplicadas
        validaCozinha(); //Valida tipo de cozinha
        
        System.out.println(cousineTypes.toString());
        System.out.println("Tipos de cozinha: " + cousineTypes.size());
        System.out.println("Número de restaurantes: " + rests.size());
        
        int i = 1;
        for(Restaurant r : rests.values()) {
            //System.out.println(i + ": " + r.name + "       " + r.addr + "        " + r.contact + "       " + r.cousine.toString() + "     " + r.price);
            //System.in.read();
            //i++;
            DAO_REST.save(r);
        }
    }
    
    
    
    
    
    private static void validaCozinha() {
        ArrayList<String> toRemove = new ArrayList<>();
        for(String k : rests.keySet()) {
            ArrayList<String> temp = rests.get(k).cousine;
            
            for(String s : temp) {
                if(!cousineTypes.contains(s)) {
                    if(s.contains("Nogueiró") || s.contains("São José de São Lázaro")) toRemove.add(k);
                    else cousineTypes.add(s);
                }
            }
        }
        for(int i = 0; i < toRemove.size(); i++) rests.remove(toRemove.get(i));    }
    
    
    public static void processaDados() throws IOException, ParseException {
        JSONParser parser = new JSONParser();
        Object obj = parser.parse(new FileReader("C:\\Users\\Carlos\\Desktop\\Yum-Yum\\JSON\\jsonReader\\src\\res\\166rest.json"));
        JSONObject jsonObject = (JSONObject) obj;

        
        ArrayList<String> cousine;
        ArrayList<String> comments;
        int j;



        for(Object key : jsonObject.keySet()) {
            JSONObject rest = (JSONObject) jsonObject.get(key);
            cousine = new ArrayList<>();
            comments = new ArrayList<>();
            String[] strs = rest.get("cousine_type").toString().split("\"");

            if(rest.get("restaurant_comments") != null) {
                for(j = 0; j < strs.length; j++) {
                    switch(strs[j]) {
                        case "[":
                        case "]":
                        case ",":
                        case "N/A":
                            break;
                        default:
                            cousine.add(strs[j]);
                            break;
                    }
                }
                
                if(cousine.size() > 0) {
                    strs = rest.get("restaurant_comments").toString().split("\"");
                    boolean join = false;
                    int index = 0;
                    for(j = 0; j < strs.length; j++) {
                        switch(strs[j]) {
                            case "[":
                            case "]":
                            case ",":
                            case "N/A":
                                break;
                            default:
                                if(join && index != 0) {
                                    comments.add(comments.remove(index-1) + strs[j]);
                                    join = false;
                                }
                                if(strs[j].length() < 100 && index != 0) {
                                    comments.add(comments.remove(index-1) + strs[j]);
                                    join = true;
                                }
                                else {
                                    if(strs[j].charAt(0) == ' ' ||
                                       strs[j].charAt(0) == ',' ||
                                       strs[j].charAt(0) == '/' ||
                                       strs[j].charAt(0) == '\\'||
                                       strs[j].charAt(0) == '.') {
                                        comments.add(comments.remove(index-1) + strs[j]);
                                    }
                                    else {
                                        if(!strs[j].contains("agradece") && !strs[j].contains("Muito obrigado") && !strs[j].contains("tivemos um problema no quadro")) {
                                            comments.add(strs[j]);
                                            index++;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }

                
                if(comments.size() > 0 && !rest.get("restaurant_contact").equals("N/A") && !rest.get("price_range").equals("N/A")) {
                    rests.put((String) rest.get("restaurant_name"),
                                     new Restaurant(
                                             (String) rest.get("restaurant_name"),
                                             (String) rest.get("restaurant_address"),
                                             (String) rest.get("restaurant_contact"),
                                             (String) rest.get("restaurant_rating"),
                                             (String) rest.get("restaurant_foto"),
                                             (String) rest.get("price_range"),
                                             comments,
                                             cousine,
                                             rests.size()
                                     ));
                }
            }
        }
    }

    private static void verificaDuplicados() {
        rests.forEach((k, v) -> {
           for(String key : rests.keySet()) {
               if(key.equals(k)) {
                   if(v.index != rests.get(key).index) {
                       rests.remove(key);
                       System.out.println("Duplicado " + v.name + " removido.");
                   }
               }
           } 
        });
    }
}