import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 1},
    ],
    thresholds: {
        http_req_duration: ['p(95)<750'],  // Ensure 95% of requests complete in <750ms
    },
};

export default function () {
    //const url = 'https://localhost:44313/api/ElderlyUser/GetElderlyUserDetails/user37@example.com'; 
    const url = 'https://localhost:44313/api/ElderlyCareSupportAccount/GetFeeDetails'
    //const token = 'eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJycWtPSVMtbHM5S3hvdEJLTHJsUDZNaG92SS00cmNUMDFjV0F2YlNIOFowIn0.eyJleHAiOjE3MzQ0NDc3NzYsImlhdCI6MTczNDQ0NTk3NiwianRpIjoiZjFhMTQ2OTAtNjM1MC00YjJjLWEyNDctY2Y5YjJlMTY5OTkwIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9FbGRlcmx5Q2FyZVJlYWxtIiwiYXVkIjpbIkVsZGVybHlDYXJlQWNjb3VudENsaWVudCIsImJyb2tlciIsImFjY291bnQiXSwic3ViIjoiZTNkOTkxNWUtNmZjZi00ODQ3LTljOWQtZTlkZjI0OGQ0MDNhIiwidHlwIjoiQmVhcmVyIiwiYXpwIjoiRWxkZXJseUNhcmVBY2NvdW50Q2xpZW50Iiwic2lkIjoiNDE2MGQyOTgtZTA2Ny00NzgyLTk5YzMtZTlkODA1MWNkYTkyIiwiYWNyIjoiMSIsImFsbG93ZWQtb3JpZ2lucyI6WyIvKiJdLCJyZWFsbV9hY2Nlc3MiOnsicm9sZXMiOlsib2ZmbGluZV9hY2Nlc3MiLCJkZWZhdWx0LXJvbGVzLWVsZGVybHljYXJlcmVhbG0iLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7IkVsZGVybHlDYXJlQWNjb3VudENsaWVudCI6eyJyb2xlcyI6WyJ1bWFfcHJvdGVjdGlvbiJdfSwiYnJva2VyIjp7InJvbGVzIjpbInJlYWQtdG9rZW4iXX0sImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoib3BlbmlkIGVtYWlsIG1pY3JvcHJvZmlsZS1qd3Qgb2ZmbGluZV9hY2Nlc3MgcHJvZmlsZSIsInVwbiI6InNlcnZpY2UtYWNjb3VudC1lbGRlcmx5Y2FyZWFjY291bnRjbGllbnQiLCJlbWFpbF92ZXJpZmllZCI6ZmFsc2UsImNsaWVudEhvc3QiOiIxNzIuMTcuMC4xIiwiZ3JvdXBzIjpbIm9mZmxpbmVfYWNjZXNzIiwiZGVmYXVsdC1yb2xlcy1lbGRlcmx5Y2FyZXJlYWxtIiwidW1hX2F1dGhvcml6YXRpb24iXSwicHJlZmVycmVkX3VzZXJuYW1lIjoic2VydmljZS1hY2NvdW50LWVsZGVybHljYXJlYWNjb3VudGNsaWVudCIsImNsaWVudEFkZHJlc3MiOiIxNzIuMTcuMC4xIiwiY2xpZW50X2lkIjoiRWxkZXJseUNhcmVBY2NvdW50Q2xpZW50In0.XEEfoEUpSgDr66t8rvK2AcOuzQKmHLlkPfBAmYB5KOnNTu_-MbpkFXwQhH4iNohOq7pUE-M38-2tGSxFnnjHIgv0RIMXCe0LuoE_mbVLtWEixaL-0_qfgt9bvkJpE9JkFgOLlvE3ava3uqM1CRXU3LNXZi37EshbD_arcZzwWt-hYl1NRdlxtxpCuRwo8DgCddvNMp7kinXcyLU0i5ftqbk-n-3GAnyGrK4yZ_AzBzlHn4EirK5o35NxsjbYthexVDCP38Gh6yxT8a-HI7HClVHo_9PTJ2C-8LwqJpo1B-GeF2RUlUSkZakpoGdADI0N7O3PAzr2X3p130CVvE27Iw';
    const params = {
        headers: {
            'content-type': 'application/json',
            //'Authorization': 'Bearer ' + token,
        },
    };

    // Perform the POST request
    const res = http.get(url, {headers: params.headers});

    // Validate the response
    check(res, {
        'is status 200': (r) => r.status === 200,
        'response time < 500ms': (r) => r.timings.duration < 500,
    });

    console.log(`Response: ${res.status} - ${res.body}`);
    
    sleep(1);
}
