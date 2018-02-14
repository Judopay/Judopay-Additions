terragrunt {
  # Configure Terragrunt to automatically store tfstate files in S3
  remote_state {
    backend = "s3"

    config {
      encrypt = true
      bucket  = "judo-terraform-state"
      key     = "${get_env("TF_VAR_environment", "")}/${get_env("TF_VAR_region","")}/services/${get_env("TF_VAR_service_name","")}/state.tfstate"
      region  = "eu-west-1"

      # Configure dynamodb locking
      lock_table = "terraform-euwest1-${get_env("TF_VAR_environment", "")}"
    }
  }
}
