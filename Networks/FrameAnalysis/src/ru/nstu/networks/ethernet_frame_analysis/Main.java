package ru.nstu.networks.ethernet_frame_analysis;

import java.io.*;
import java.util.*;

public class Main {
    public static void main(String[] args) throws FileNotFoundException {
        Scanner scanner = new Scanner(System.in);

        final String currentFolderPath = "src/ru/nstu/networks/ethernet_frame_analysis";
        final String binaryExtension = ".bin";

        File folder = new File(currentFolderPath + "/resources");
        List<String> fileNames = Arrays.stream(Objects.requireNonNull(folder.listFiles()))
                .filter(file -> file.getName().endsWith(binaryExtension))
                .sorted(Comparator.comparing(File::getName))
                .map(File::getName).toList();

        if (fileNames.isEmpty()) {
            throw new FileNotFoundException("The resource folder does not contain files for analysis");
        }

        System.out.println("Files available for analysis:");
        fileNames.forEach(System.out::println);

        File file;

        while (true) {
            try {
                System.out.printf("%nEnter file name: ");
                String fileName = scanner.nextLine();

                if (!fileNames.contains(fileName)) {
                    throw new FileNotFoundException("The resources folder does not contain a file called \""
                            + fileName + "\"");
                }

                file = new File(currentFolderPath + "/resources/" + fileName);
                break;
            } catch (FileNotFoundException e) {
                System.out.println("Error: " + e.getMessage() + ". Try again");
            }
        }

        System.out.println("Where to output the result?");
        System.out.println("Enter '1' - to print the results to the console");
        System.out.println("Enter any other character - to output the results to the 'out.txt' file.");

        PrintStream printStream = Objects.equals(scanner.nextLine(), "1")
                ? System.out
                : new PrintStream(new FileOutputStream(currentFolderPath + "/out.txt"));
        System.out.printf("Let's launch...%n%n");
        EthernetFrameAnalyzer.analyze(file, printStream);
        System.out.println("Ready!");
    }
}