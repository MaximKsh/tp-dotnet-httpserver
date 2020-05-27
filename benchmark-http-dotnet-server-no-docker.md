# TpDotnetServer

Hardware: i5-6200U, DDR4-2133, SSD

```
$ ab -n 1000 -c 100 http://localhost:5000/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        http-dotnet-server
Server Hostname:        localhost
Server Port:            5000

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.465 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      954976000 bytes
HTML transferred:       954824000 bytes
Requests per second:    2148.56 [#/sec] (mean)
Time per request:       46.543 [ms] (mean)
Time per request:       0.465 [ms] (mean, across all concurrent requests)
Transfer rate:          2003733.66 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.9      1      10
Processing:     6   45  23.6     39     133
Waiting:        0    8  13.1      2      78
Total:         16   46  23.8     40     136

Percentage of the requests served within a certain time (ms)
  50%     40
  66%     42
  75%     45
  80%     46
  90%     69
  95%    118
  98%    131
  99%    133
 100%    136 (longest request)
maxim@maximPC:~/DotnetProjects/tp-dotnet-httpserver$ ab -n 1000 -c 100 http://localhost:5000/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        http-dotnet-server
Server Hostname:        localhost
Server Port:            5000

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.473 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      954976000 bytes
HTML transferred:       954824000 bytes
Requests per second:    2116.27 [#/sec] (mean)
Time per request:       47.253 [ms] (mean)
Time per request:       0.473 [ms] (mean, across all concurrent requests)
Transfer rate:          1973618.08 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.7      1      11
Processing:     8   45  25.8     38     143
Waiting:        0    9  13.3      3      57
Total:         17   46  26.0     39     148

Percentage of the requests served within a certain time (ms)
  50%     39
  66%     43
  75%     50
  80%     56
  90%     78
  95%    118
  98%    133
  99%    133
 100%    148 (longest request)
maxim@maximPC:~/DotnetProjects/tp-dotnet-httpserver$ ab -n 1000 -c 100 http://localhost:5000/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        http-dotnet-server
Server Hostname:        localhost
Server Port:            5000

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.472 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      954976000 bytes
HTML transferred:       954824000 bytes
Requests per second:    2120.81 [#/sec] (mean)
Time per request:       47.152 [ms] (mean)
Time per request:       0.472 [ms] (mean, across all concurrent requests)
Transfer rate:          1977858.17 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.7      1       8
Processing:     7   45  25.0     40     135
Waiting:        0   10  13.8      5      62
Total:         13   46  25.1     40     139

Percentage of the requests served within a certain time (ms)
  50%     40
  66%     42
  75%     43
  80%     53
  90%     84
  95%    107
  98%    129
  99%    133
 100%    139 (longest request)
maxim@maximPC:~/DotnetProjects/tp-dotnet-httpserver$ ab -n 1000 -c 100 http://localhost:5000/httptest/wikipedia_russia.html
This is ApacheBench, Version 2.3 <$Revision: 1757674 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 100 requests
Completed 200 requests
Completed 300 requests
Completed 400 requests
Completed 500 requests
Completed 600 requests
Completed 700 requests
Completed 800 requests
Completed 900 requests
Completed 1000 requests
Finished 1000 requests


Server Software:        http-dotnet-server
Server Hostname:        localhost
Server Port:            5000

Document Path:          /httptest/wikipedia_russia.html
Document Length:        954824 bytes

Concurrency Level:      100
Time taken for tests:   0.481 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      954976000 bytes
HTML transferred:       954824000 bytes
Requests per second:    2079.07 [#/sec] (mean)
Time per request:       48.099 [ms] (mean)
Time per request:       0.481 [ms] (mean, across all concurrent requests)
Transfer rate:          1938924.81 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    1   1.7      1       8
Processing:     6   46  23.8     41     151
Waiting:        0    5   9.2      1      52
Total:         11   47  24.9     42     155

Percentage of the requests served within a certain time (ms)
  50%     42
  66%     42
  75%     43
  80%     47
  90%     77
  95%    121
  98%    133
  99%    151
 100%    155 (longest request)

```
