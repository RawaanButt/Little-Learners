import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Scanner;

public class Main {

    String inputFileName = "NFA.txt";
    public int row;
    public int col;
     int[][] a = new int[4][2];
     int[][] b = new int[4][2];
     int[][] dfadata =  new int[12][];
    ArrayList<String> filedata = new ArrayList<>();
    ArrayList<Integer> finalStates = new ArrayList<>();

    public void read() {

//Fetching NFa data from input File
        try {
            Path path = Paths.get(inputFileName);
            Scanner scanner = new Scanner(path);

            int counter = 0;
             while (scanner.hasNextLine()) {
                 if(counter == 0)
                 {
                     String line = scanner.nextLine();
                     this.row= Character.getNumericValue(line.charAt(0));
                     this.col= Character.getNumericValue(line.charAt(2));

                    counter++;
                 }else{
                     String line = scanner.nextLine();
                     filedata.add(line);
                     counter++;
                 }
             }

            finalStates.add(1);
            finalStates.add(3);

             int checker=0;
             int pointerB =0;
             int pointerA =0;
             for(int i=0;i<filedata.size();i++){

                  int temp =0;
                  for(int k=4;k<filedata.get(i).length();k++){
                       if(i%2 == 0){
                           a[pointerA][temp] = Character.getNumericValue(filedata.get(i).charAt(k));
                           temp++;
                       }else{
                           b[pointerB][temp] = Character.getNumericValue(filedata.get(i).charAt(k));
                           temp++;
                        }
                       k++;
                   }

                 if(i%2==0){
                         pointerA++;
                     }else{
                         pointerB++;
                     }
            }


        System.out.println("Transition Table For Given NFA:");
        System.out.println("      A          B");
        System.out.println("_________________________");
           for(int i=0 ; i<a.length;i++){
               System.out.print(i+"   ");
                for (int j=0;j<a[i].length;j++)
                 {
                     if(a[i][j]== 0){
                         System.out.print("");
                     }
                     else if(a[i][j]== 23){
                         System.out.print("  [NA]");
                     }else {
                         System.out.print("["+a[i][j]+"]");
                     }
                 }
               System.out.print("      ");
                for (int k=0;k<b[i].length;k++)
                 {

                     if(b[i][k]== 0){
                         System.out.print("");
                     }
                     else if(b[i][k]== 23){
                         System.out.print("[NA]");
                     }else{
                         System.out.print("["+b[i][k]+"]");

                     }
                 }
               System.out.println();
           }

         System.out.print("Final States are :");
         for(int i=0;i<finalStates.size();i++)
           {
               System.out.print(finalStates.get(i)+",");
           }
            System.out.println();
            System.out.println();
            System.out.println();
            System.out.println();

            scanner.close();
        }catch (Exception e){}
    }


 public static void main(String args[]){
     try {
         Path path = Paths.get(inputFileName);
         Scanner scanner = new Scanner(path);

         int counter = 0;
         while (scanner.hasNextLine()) {
             if(counter == 0)
             {
                 String line = scanner.nextLine();
                 this.row= Character.getNumericValue(line.charAt(0));
                 this.col= Character.getNumericValue(line.charAt(2));

                 counter++;
             }else{
                 String line = scanner.nextLine();
                 filedata.add(line);
                 counter++;
             }



       int counter = 0;
       int a1[] = {0};
       int b1[] = {1};
       int c1[] = {2};
       int d1[] = {3};
       dfadata[counter] = a1;
       counter++;
       dfadata[counter] = b1;
       counter++;
       dfadata[counter] = c1;
       counter++;
       dfadata[counter] = d1;
       counter++;
       for (int i = 0; i < a.length; i++) {

           dfadata[counter] = a[i];
           counter++;
       }
       for (int i = 0; i < b.length; i++) {

           dfadata[counter] = b[i];
           counter++;
       }

       System.out.println("Transition Table OF DFA From Given Nfa After appling Subset Construction Method :");
       System.out.println();
       System.out.println("           "+"A"+"         "+"B");
       System.out.println("____________________________");


       for (int c =0; c < 4; c++) {
           System.out.print("[");
           for (int j = 0; j < dfadata[c].length; j++) {
               System.out.print(dfadata[c][j]);
           }
           System.out.print("]");
           System.out.print("      ");
           System.out.print("[");

           for (int i = 0; i < a[c].length; i++){


               if (a[c][i] == 23) {
                       System.out.print("NA");
             }else if (i != 0 && a[c][i] == 0) {
                      System.out.print("");
                   }else{
                       System.out.print(a[c][i]);
                   }

           }
               System.out.print("]");
               System.out.print("      ");

               System.out.print("[");
               for (int j = 0; j < b[c].length; j++) {

                   if (b[c][j] == 23) {
                       System.out.print("NA");
                       break;
                   }else {
                       if (j != 0 && b[c][j] == 0) {
                           System.out.print("");
                       } else {
                           System.out.print(b[c][j]);
                       }
                   }
               }
               System.out.print("]");
               System.out.println();

       }

       for (int i = 4; i <( dfadata.length-2); i++) {

           if(dfadata[i][0]== 23){

               continue;
           }


           System.out.print("[");
           for (int j = 0; j < dfadata[i].length; j++) {
               System.out.print(dfadata[i][j]);
           }
           System.out.print("]");
           System.out.print("      ");

           System.out.print("[");
           for (int j = 0; j < dfadata[i].length; j++) {
               for (int k = 0; k < a[dfadata[i][j]].length; k++) {
                   if (a[dfadata[i][j]][k] == 23) {
                       System.out.print("");
                   } else if (a[dfadata[i][j]][k] == 0) {
                       System.out.print("");
                   } else {
                       System.out.print(a[dfadata[i][j]][k]);
                   }
               }
           }
           System.out.print("]");
           System.out.print("      ");
           System.out.print("[");
           for (int j = 0; j<dfadata[i].length; j++) {
               for (int k = 0; k < b[dfadata[i][j]].length; k++) {
                   if(b[dfadata[i][j]][k] == 23) {
                       System.out.print("");
                   }else if(b[dfadata[i][j]][k] == 0){
                       System.out.print("");
                   }else{
                       System.out.print(b[dfadata[i][j]][k]);

                   }
               }
           }
           System.out.print("]");

           System.out.println();

       }
       System.out.println("[NA]"+"      "+"[NA]"+"       "+"[NA]");


    }


}

