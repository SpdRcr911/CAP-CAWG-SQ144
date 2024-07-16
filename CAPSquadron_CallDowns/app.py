from flask import Flask, request, jsonify, render_template
import requests

api_client_base_url = 'http://192.168.10.170:5203'
app = Flask(__name__)

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/api/attendies', methods=['GET'])
def get_attendies():
    response = requests.get(f'{api_client_base_url}/api/AttendanceSignIn')
    if response.status_code == 200:
        data = response.json()
        # Filter and sort the data
        filtered_sorted_data = sorted(
            [item for item in data if item['rank'].startswith('C/')],
            key=lambda x: x['name']
        )
        options = [{'capid': item['capid'], 'name': f"{item['rank']} {item['name']}"} for item in filtered_sorted_data]
        return jsonify({'attendies': options})
    else:
        return jsonify({'message': 'Error fetching attendies'}), 500

@app.route('/api/currentAgenda')
def get_agenda():
    return { 'title': '2nd Tuesday - AE', 'uod': 'BDU', 'notes': '' }

@app.route('/submit', methods=['POST'])
def submit():
    data = request.get_json()
    name = data.get('name')
    email = data.get('email')
    dropdown = data.get('dropdown')

    # Simulate API call
    response = requests.post('https://api.example.com/data', json={'name': name, 'email': email, 'dropdown': dropdown})

    if response.status_code == 200:
        return jsonify({'message': 'Success'}), 200
    else:
        return jsonify({'message': 'Error'}), 500

if __name__ == '__main__':
    app.run(debug=True)
