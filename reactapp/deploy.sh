# Check if the username is provided
if [ -z "$1" ]; then
  echo "Error: No username provided."
  echo "Usage: ./deploy.sh [username]"
  exit 1
fi

# Switch to the main branch
#echo "Switching to main branch ..."
#git checkout main

# Assign the first argument to a variable
deployUser=$1

echo "building app ..."
npm run build

echo "deploying app ..."
scp -r dist/* "$deployUser"@164.92.149.13:/var/www/sem3-5b.com/html

# requires ssh key
echo "Restarting Nginx on the server..."
ssh "$deployUser"@164.92.149.13 'sudo systemctl restart nginx'

echo "done"
