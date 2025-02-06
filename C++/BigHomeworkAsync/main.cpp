#include <pthread.h>
#include <unistd.h>
#include <iostream>
#include <vector>
#include <random>

// Struct for monk with ci, number and mutex
struct Monk {
    Monk(int number, int ci) : number(number), ci(ci) {
        pthread_mutex_init(&monkMutex, nullptr);
    }

    ~Monk() { pthread_mutex_destroy(&monkMutex); };

    int number;
    int ci;
    pthread_mutex_t monkMutex;
};

// Global variables
std::vector<Monk *> monks;
pthread_mutex_t monks_mutex = PTHREAD_MUTEX_INITIALIZER;

// Class with methods for input data
class Config {
public:
    // Generate random monks and fill vector with they
    void randomGenerate(int left, int right, int N) {
        std::random_device random_device;
        std::mt19937 rand(random_device());
        std::uniform_int_distribution<> distrib(left, right);
        int count = 1;
        for (int i = 0; i < N; i++) {
            monks.push_back(new Monk(count++, distrib(rand)));
        }
    }

    // Fill vector with values from string of numbers
    void fillMonks(std::string str) {
        std::vector<std::string> nums_string = split(str);
        int count = 1;
        for (auto &i: nums_string) {
            monks.push_back(new Monk(count++, std::stoi(i)));
        }
    }

private:
    // Internal function to split a string with delimiter
    std::vector<std::string> split(std::string &s, std::string delimiter = ",") {
        size_t pos_start = 0, pos_end, delim_len = delimiter.length();
        std::string token;
        std::vector<std::string> res;

        while ((pos_end = s.find(delimiter, pos_start)) != std::string::npos) {
            token = s.substr(pos_start, pos_end - pos_start);
            pos_start = pos_end + delim_len;
            res.push_back(token);
        }

        res.push_back(s.substr(pos_start));
        return res;
    }
};

// Function for threads
void *fight(void *args) {
    while (true) {
        pthread_mutex_lock(&monks_mutex);
        // No pairs of monks in pool - stop
        if (monks.size() < 2) {
            pthread_mutex_unlock(&monks_mutex);
            break;
        }
        // Choose two monks
        Monk *monk1 = monks.back();
        monks.pop_back();
        Monk *monk2 = monks.back();
        monks.pop_back();
        pthread_mutex_unlock(&monks_mutex);
        // Calculate fight time
        auto fight_time = static_cast<long long>(1000 * static_cast<double>(
                                                     std::min(monk1->ci, monk2->ci)) / std::max(monk1->ci, monk2->ci));

        usleep(fight_time);
        pthread_mutex_lock(&monk1->monkMutex);
        pthread_mutex_lock(&monk2->monkMutex);
        // Fight of monks - write to console a result
        if (monk1->ci >= monk2->ci) {
            printf(
                "Monk %d with %d ci energy and monk %d with %d ci energy fights! First monk wins, %lld ms of time was spend.\n",
                monk1->number, monk1->ci, monk2->number, monk2->ci, fight_time);
            monk1->ci += monk2->ci;
            monk2->ci = -1;
        } else {
            printf(
                "Monk %d with %d ci energy and monk %d with %d ci energy fights! Second monk wins, %lld ms of time was spend.\n",
                monk1->number, monk1->ci, monk2->number, monk2->ci, fight_time);
            monk2->ci += monk1->ci;
            monk1->ci = -1;
        }
        // Unlock mutexes
        pthread_mutex_unlock(&monk2->monkMutex);
        pthread_mutex_unlock(&monk1->monkMutex);
        pthread_mutex_lock(&monks_mutex);
        // Return a winner to pool
        if (monk1->ci != -1) {
            monks.push_back(monk1);
        } else if (monk2->ci != -1) {
            monks.push_back(monk2);
        }
        pthread_mutex_unlock(&monks_mutex);
    }
    return nullptr;
}

int main(int argc, char *argv[]) {
    Config config;
    // Work with arguments
    for (int i = 1; i < argc; ++i) {
        std::string argument = argv[i];

        if (argument.find('-') == 0) {
            if (argument == "-random") {
                try {
                    int left = std::stoi(argv[i + 1]);
                    int right = std::stoi(argv[i + 2]);
                    int N = std::stoi(argv[i + 3]);
                    config.randomGenerate(left, right, N);
                } catch (...) {
                    std::cerr << "Wrong parameters!" << '\n';
                    return 1;
                }
            }
            if (argument == "-input") {
                try {
                    config.fillMonks(argv[i + 1]);
                } catch (...) {
                    std::cerr << "Wrong parameters!" << '\n';
                    return 1;
                }
            }
        }
    }
    // Number of threads - we can change this value to optimise work of a program
    const int number_of_threads = 10;
    pthread_t threads[number_of_threads];
    // Create threads
    for (int i = 0; i < number_of_threads; ++i) {
        pthread_create(&threads[i], nullptr, fight, nullptr);
    }
    // Start work of a threads
    for (int i = 0; i < number_of_threads; ++i) {
        pthread_join(threads[i], nullptr);
    }

    pthread_mutex_lock(&monks_mutex);
    if (!monks.empty()) {
        printf("Fight is over! Best monk has number %d, his Ci energy equals %d \n", monks[0]->number, monks[0]->ci);
    } else {
        std::cout << "All monks defeated, there are no winners... \n";
    }
    pthread_mutex_unlock(&monks_mutex);
    // Clear a memory
    for (auto monk: monks) {
        delete monk;
    }
    pthread_mutex_destroy(&monks_mutex);

    return 0;
}
